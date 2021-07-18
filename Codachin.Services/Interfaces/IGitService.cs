using Codachin.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codachin.Services
{
    public interface IGitService : IDisposable
    {
        public IGitService Init(string gitUri);

        public Task<IEnumerable<Commit>> GetLogAsync();

        public Task<IEnumerable<Commit>> GetLogAsync(PaginationFilter pager);

    }
}