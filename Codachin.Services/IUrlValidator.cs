using System;

namespace Codachin.Services
{
    public interface IUrlValidator
    {
        public Tuple<string, string> ValidateUrl(string gitUri);

    }
}