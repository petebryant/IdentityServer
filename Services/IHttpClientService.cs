using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> GetWithTokenAsync(string baseUri, string requestUri, string clientId, int lifetime, IEnumerable<string> scopes = null, IEnumerable<string> audiences = null);
    }
}
