using Codachin.Services;
using Codachin.Services.dto;
using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Linq;

namespace Codachin
{
    class Program
    {
        // directory of the git repository
        private static readonly string _directory = "C:\\Users\\Pandinni\\Programming\\interviews\\codacy\\gitrepo";
        static void Main(string[] args)
        {

            if (args == null || args.Length <= 1)
            {
                System.Console.WriteLine("Invalid input. Please enter the input in the form -> \"PriceBasket item1 item2 item3 …\"");
                return;
            }

            var repositoryInfo = GitCliService.GetRepositoryInformationForPath(_directory);

            try
            {

                OperationManager operationManager = new OperationManager();

                if (!operationManager.HasOperation(args[0]))
                {
                    return;
                }

                operationManager.ExecuteOperation(args[0], args.Skip(1));

            }catch(Exception e)
            {

            }

            foreach (Commit item in repositoryInfo.Log)
            {
                Console.WriteLine(item);
            }
         
        }
    }
}
