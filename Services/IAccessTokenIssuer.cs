using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public interface IAccessTokenIssuer
    {
        Task<string> IssueTokenAsync(string clientId, int lifetime, IEnumerable<string> scopes = null, IEnumerable<string> audiences = null);
    }
}
