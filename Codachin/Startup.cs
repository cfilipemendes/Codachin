using Codachin.Services;
using Codachin.Services.Dto;
using Codachin.Services.Exceptions;
using CommandLine;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Codachin
{
    public class Startup
    {
        
        public static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<CommandLineOptions>(args)
            .MapResult(async (CommandLineOptions opts) =>
            {
                try
                {
                    using IGitService _gitService = new GitCliService(new GitUrlValidator()).Init(args[0]);
                    var commmitList = await _gitService.GetLogAsync(new PaginationFilter(opts.Page,opts.LimitPerPage));
                    foreach (var item in commmitList)
                    {
                        Console.WriteLine(item);
                    }
                    return 0;
                }
                catch (GitException e)
                {
                    Console.WriteLine(e.Message);
                    return -2;
                }
            },
            // Invalid arguments
            errs => Task.FromResult(-1));
        }
    }
}
