using Codachin.Services.Exceptions;
using System;

namespace Codachin.Services
{
    public class GitUrlValidator : IUrlValidator
    {
        public Tuple<string, string> ValidateUrl(string gitUrl)
        {
            if (gitUrl == null)
            {
                throw new ArgumentNullException("Uri can't be null, please write a valid git url.");
            }

            try
            {
                Uri uri = new Uri(gitUrl);
                var gitUser = uri.Segments[1].Remove(uri.Segments[1].Length - 1);
                var repository = uri.Segments[2].Split(".git")[0];
                return new Tuple<string, string>(gitUser, repository);
            }
            catch (Exception)
            {
                throw new GitException($"Url malformed, it should be something like https://github.com/<user>/<repo>.git and was {gitUrl}.");
            }
        }
    }
}
