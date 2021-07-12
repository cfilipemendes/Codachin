using Codachin.Services.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Codachin.Services
{
    public class GitCliService : IDisposable
    {
        private readonly Process _gitProcess;
        private bool _disposed;

        public static GitCliService GetRepositoryInformationForPath(string path)
        {
            var repositoryInformation = new GitCliService(path);
            if (repositoryInformation.IsGitRepository)
            {
                return repositoryInformation;
            }
            throw new ArgumentException($"The location {path} is not a git repository ");
        }



        public IEnumerable<Commit> Log
        {
            get
            {
                int skip = 0;
                while (true)
                {
                    var entry = RunCommand(String.Format("log --skip={0} -n1", skip++));

                    if (String.IsNullOrWhiteSpace(entry))
                    {
                        yield break;
                    }

                    yield return Commit.Parse(entry);
                }
            }
        }


        private GitCliService(string path)
        {

            if (path == null || !Directory.Exists(path))
            {
                throw new ArgumentException($"Directory path must exists and should be a valid path, provided path was -> {path}");
            }

            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "git.exe",
                CreateNoWindow = true,
                WorkingDirectory = (path != null && Directory.Exists(path)) ? path : Environment.CurrentDirectory
            };

            _gitProcess = new Process();
            _gitProcess.StartInfo = processInfo;

        }

         private bool IsGitRepository
         {
            get
            {
                return !String.IsNullOrWhiteSpace(RunCommand("log -1"));
            }
         }

        private string RunCommand(string args)
        {
            _gitProcess.StartInfo.Arguments = args;
            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd();
            _gitProcess.WaitForExit();
            return output;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }

    }
}
