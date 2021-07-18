using System.Net.Http;
using System.Threading.Tasks;

namespace Codachin.Services.Interfaces
{
    public interface IHttpNetWrapper
    {
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}
