using CommandLine;

namespace Codachin
{
    public class CommandLineOptions
    {
        [Value(index: 0, Required = true, HelpText = "Git Url Path to retrieve commits.")]
        public string GitUrl { get; set; }

        [Option(shortName: 'p', longName: "Page", Required = false, HelpText = "Page to get commits.", Default = 1)]
        public int Page { get; set; }

        [Option(shortName: 'l', longName: "Limit Per Page", Required = false, HelpText = "Number of commits per page", Default = 100)]
        public int LimitPerPage { get; set; }
    }
}
