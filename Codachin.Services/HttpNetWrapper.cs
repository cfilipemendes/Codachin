using Codachin.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Codachin.Services
{
    public class HttpNetWrapper : IHttpNetWrapper
    {
        private readonly string baseUri;
        private HttpClient client;

        public HttpNetWrapper(string gitApiBaseUri)
        {
            this.baseUri = gitApiBaseUri;
            client = new HttpClient();
            //These headers are required so our request to GITHUB open API doesn't give us a 403 forbidden response.
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("User-Agent", @"Mozilla");
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            return await client.GetAsync(baseUri + path);
        }
    }
}
