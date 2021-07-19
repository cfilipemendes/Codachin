using System;

namespace Codachin.Services
{
    public interface IUrlValidator
    {

        /// <exception cref="Exceptions.GitException">If the URL is invalid</exception>
        public Tuple<string, string> ValidateUrl(string gitUri);

    }
}