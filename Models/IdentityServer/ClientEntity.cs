using IdentityServer4.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer.Models.IdentityServer
{
    public class ClientEntity : Client
    {
        public string UserClaims
        {
            get => JsonConvert.SerializeObject(this.Claims);
            set => this.Claims = JsonConvert.DeserializeObject<ICollection<Claim>>(value);
        }

        public string ClientAllowedGrantTypes
        {
            get => JsonConvert.SerializeObject(this.AllowedGrantTypes);
            set => this.AllowedGrantTypes = JsonConvert.DeserializeObject<ICollection<string>>(value);
        }

        public string ClientRedirectUris
        {
            get => JsonConvert.SerializeObject(this.RedirectUris);
            set => this.RedirectUris = JsonConvert.DeserializeObject<ICollection<string>>(value);
        }

        public string ClientPostLogoutRedirectUris
        {
            get => JsonConvert.SerializeObject(this.PostLogoutRedirectUris);
            set => this.PostLogoutRedirectUris = JsonConvert.DeserializeObject<ICollection<string>>(value);
        }

        public string ClientAllowedScopes
        {
            get => JsonConvert.SerializeObject(this.AllowedScopes);
            set => this.AllowedScopes = JsonConvert.DeserializeObject<ICollection<string>>(value);
        }
        public string ClientAllowedCorsOrigins
        {
            get => JsonConvert.SerializeObject(this.AllowedCorsOrigins);
            set => this.AllowedCorsOrigins = JsonConvert.DeserializeObject<ICollection<string>>(value);
        }

        public string Secret
        {
            get
            {
                if (this.ClientSecrets.Any())
                    return JsonConvert.SerializeObject(this.ClientSecrets.Select(s => new Secret(s.Value)));
                else
                    return string.Empty;

            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    ClientSecrets = JsonConvert.DeserializeObject<ICollection<Secret>>(value);
            }
        }
    }
}
