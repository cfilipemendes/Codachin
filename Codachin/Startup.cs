using Codachin.Services;
using Codachin.Services.Exceptions;
using CommandLine;
using System;
using System.IO;

namespace Codachin
{
    public class Startup
    {

        public static int Main(string[] args)
        {
            try
            {
                using (var _gitService = GitCliService.GetRepositoryInformationForPath(Directory.GetCurrentDirectory()))
                {
                    return CommandLine.Parser.Default.ParseArguments<Log, Checkout>(args)
                        .MapResult(
                        (Log opts) => RunLogAndReturnExitCode(opts,args,_gitService),
                        (Checkout opts) => RunCheckoutAndReturnExitCode(opts, args, _gitService),
                        errs => 1);

                }

            }
            catch (GitException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }

        private static int RunCheckoutAndReturnExitCode(Checkout opts, string[] args, GitCliService gitCliService)
        {
            if (args[1] == null || args.Length > 2)
            {
                Console.WriteLine("Checkout command wrong. Ex: cc checkout <branchname>");
            }
            gitCliService.BranchName = args[1];
            Console.WriteLine($"Current branch is {gitCliService.BranchName}");
            return 0;
        }

        private static int RunLogAndReturnExitCode(Log opts, string[] args, GitCliService gitCliService)
        {
            foreach (var item in gitCliService.Log)
            {
                Console.WriteLine(item);
            }
            return 0;
        }
    }
}
