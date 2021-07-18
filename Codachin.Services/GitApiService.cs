using Codachin.Services.Dto;
using Codachin.Services.Exceptions;
using Codachin.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Codachin.Services
{
    public class GitApiService : IGitService, IDisposable
    {
        private bool _disposed;
        
        private const string GitApiBaseUri = "https://api.github.com";
        private const string _gitApiCommitsEndpoint = "/repos/{0}/{1}/commits";

        private IUrlValidator urlValidator;
        private IHttpNetWrapper client;
        private string gitUri;

        public GitApiService(IUrlValidator urlValidator)
        {
            this.urlValidator = urlValidator;
            client = new HttpNetWrapper(GitApiBaseUri);
        }

        public async Task<IEnumerable<Commit>> GetLogAsync()
        {
            return await GetLogAsync(new PaginationFilter());
        }
        public async Task<IEnumerable<Commit>> GetLogAsync(PaginationFilter paging)
        {
            try
            {
                var gituserAndRepo = urlValidator.ValidateUrl(gitUri);
                var gitUser = gituserAndRepo.Item1;
                var repository = gituserAndRepo.Item2;

                HttpResponseMessage response = await client.GetAsync(string.Format(_gitApiCommitsEndpoint,gitUser,repository) 
                    + $"?page={paging?.Page}&per_page={paging?.PerPage}");
                if (response.IsSuccessStatusCode)
                {
                    var resultJson = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.UTF8).ReadToEnd();
                    JArray obj = JsonConvert.DeserializeObject<JArray>(resultJson);

                    List<Commit> commits = new List<Commit>();
                    foreach (var node in obj)
                    {
                        commits.Add(new Commit()
                        {
                            Sha = (string)node["sha"],
                            Author = node["author"].HasValues ? (string)node["author"]["login"] : "Unknown",
                            Date = (string)node["commit"]["author"]["date"],
                            Message = (string)node["commit"]["message"]
                        }); 
                    }
                    return commits;
                }
                else
                {
                    throw new GitException($"Invalid response {response.StatusCode} from GITHUB.API. Message was {response.ReasonPhrase}.");
                }
            }
            catch (Exception error)
            {
                //Log error.
                throw new GitException(error.Message);
            }
        }
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }

        public IGitService Init(string gitUri)
        {
            this.gitUri = gitUri;
            return this;
        }
    }
}
