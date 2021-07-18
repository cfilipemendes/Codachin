using Codachin.Services.Dto;
using System.Collections.Generic;

namespace Codachin.API.Controllers
{
    internal class PagedResponse<T> : Response<T>
    {
        public int Page { get; set; }
        public int PerPageLimit { get; set; }
        
        public PagedResponse()
        {
        }

        public PagedResponse(T data, int Page, int PerPageLimit)
        {
            Succeeded = true;
            Message = string.Empty;
            Data = data;
            this.Page = Page;
            this.PerPageLimit = PerPageLimit;
        }

    }
}