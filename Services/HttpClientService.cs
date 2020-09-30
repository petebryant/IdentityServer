using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class HttpClientService : IHttpClientService
    {
        IAccessTokenIssuer _tokenIssuer;

        public HttpClientService(IAccessTokenIssuer tokenIssuer)
        {
            _tokenIssuer = tokenIssuer;
        }

        public async Task<HttpResponseMessage> GetWithTokenAsync(string baseUri, string requestUri, string clientId, int lifetime, IEnumerable<string> scopes = null, IEnumerable<string> audiences = null)
        {
            var accessToken = await _tokenIssuer.IssueTokenAsync(clientId, lifetime, scopes, audiences);

            using (HttpClient Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(baseUri);
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Client.SetBearerToken(accessToken);

                try
                {
                    HttpResponseMessage response = await Client.GetAsync(requestUri);

                    return response;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
