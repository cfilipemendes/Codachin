using Codachin.Services.Dto;
using Codachin.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Codachin.Services
{
    public class GitCliService : IGitService, IDisposable
    {
        private IUrlValidator urlValidator;
        private readonly Process _gitProcess;
        private bool _disposed;

        public GitCliService(IUrlValidator urlValidator)
        {
            this.urlValidator = urlValidator;

            _gitProcess = new Process();
            _gitProcess.StartInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "git.exe",
                CreateNoWindow = true,
                WorkingDirectory = Environment.CurrentDirectory
            };
        }

        public Task<IEnumerable<Commit>> GetLogAsync()
        {
            return GetLogAsync(new PaginationFilter());
        }
        public Task<IEnumerable<Commit>> GetLogAsync(PaginationFilter paging)
        {

            int skip = paging.PerPage * (paging.Page - 1);
            int max_Commits = paging.PerPage * paging.Page;
            List<Commit> commitHistory = new List<Commit>();

            while (skip < max_Commits)
            {
                var entry = RunCommand(String.Format("log --skip={0} -n1", skip++));

                if (String.IsNullOrWhiteSpace(entry))
                {
                    break;
                }

                commitHistory.Add( ParseCommit(entry));
            }
            return Task.FromResult((IEnumerable<Commit>)commitHistory);
        }

        public IGitService Init(string url)
        {
            string repositoryPath = urlValidator.ValidateUrl(url).Item2;

            if (!Directory.Exists(repositoryPath) || !IsGitRepository)
            {
                RunCommand($"clone {url}");
            }

            _gitProcess.StartInfo.WorkingDirectory = repositoryPath;

            return this;
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
            _gitProcess.StartInfo.RedirectStandardError = true;
            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd();
            _gitProcess.WaitForExit();

            if (_gitProcess.ExitCode != 0) {
                throw new GitException(_gitProcess.StandardError.ReadToEnd());
            }
            return output;
        }

        private Commit ParseCommit(string commitInfo)
        {
            Commit currentCommit = new Commit();
            using (var strReader = new StringReader(commitInfo))
            {
                do
                {
                    var line = strReader.ReadLine();

                    if (line.StartsWith("commit"))
                    {
                        currentCommit.Sha = line.Split(' ')[1];
                    }
                    else if (line.StartsWith("Author:"))
                    {
                        currentCommit.Author = line.Split(": ")[1];
                    }
                    else if (line.StartsWith("Date:"))
                    {
                    
                        currentCommit.Date = line.Split(new[] { ':' }, 2)[1].TrimStart();
                    }
                    else
                    {
                        currentCommit.Message += line;
                    }
                }
                while (strReader.Peek() != -1);
            }

            return currentCommit;

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
