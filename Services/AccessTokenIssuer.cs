using IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class AccessTokenIssuer : IAccessTokenIssuer
    {
        IdentityServerTools _tools;

        public AccessTokenIssuer(IdentityServerTools tools)
        {
            _tools = tools;
        }

        public async Task<string> IssueTokenAsync(string clientId, int lifetime, IEnumerable<string> scopes = null, IEnumerable<string> audiences = null)
        {

            var accessToken = await _tools.IssueClientJwtAsync(
                clientId: clientId,
                lifetime: lifetime,
                scopes: scopes,
                audiences: audiences);

            return accessToken;
        }
    }
}
