using CommandLine;

namespace Codachin
{
    [Verb("log", HelpText = "Log branch commits.")]
    class Log{}

    [Verb("checkout", HelpText = "Checkout to a specific branch")]
    class Checkout {}

}
