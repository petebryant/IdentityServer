
using IdentityServer.Models.IdentityServer;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IdentityServer.Models.ViewModels
{
    public class ClientViewModel : IValidatableObject
    {
        private IEnumerable<string> _resources;
        private static readonly IEnumerable<string> _gTypes = new[] {
            "Code",
            "Implicit",
            "Resource Owner Password And ClientCredentials",
            "Resource Owner Password",
            "Client Credentials",
            "Hybrid And ClientCredentials",
            "Code And Client Credentials",
            "Implicit And Client Credentials" };

        public ClientViewModel()
        {
        }
        public ClientViewModel(ClientEntity client)
        {
            this.Enabled = client.Enabled;
            this.ClientId = client.ClientId;
            this.Uri = client.ClientUri;
            this.ClientName = client.ClientName;
            this.AllowedGrantTypes = SetAllowedGrandType(client.AllowedGrantTypes);
            this.ClientSecret = client.ClientSecrets.FirstOrDefault()?.Value;
            this.RedirectUri = client.RedirectUris.FirstOrDefault();
            this.AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser;
            this.PostLogoutRedirectUri = client.PostLogoutRedirectUris.FirstOrDefault();
            this.Resources = client.AllowedScopes;
            this.AllowOfflineAccess = client.AllowOfflineAccess;
            this.RefreshTokenExpiration = client.RefreshTokenExpiration;
            this.RefreshTokenUsage = client.RefreshTokenUsage;
            this.AccessTokenLifetime = client.AccessTokenLifetime;
            this.IdentityTokenLifetime = client.IdentityTokenLifetime;
            this.RequireConsent = client.RequireConsent;
            this.AlwaysSendClientClaims = client.AlwaysSendClientClaims;
            this.ClientAllowedCorsOrigin = client.AllowedCorsOrigins?.FirstOrDefault();
            this.Secret = client.ClientSecrets?.FirstOrDefault()?.Value;
            this.ConfirmationSecret = Secret;

        }


        public bool Enabled { get; set; }

        public IEnumerable<SelectListItem> GrantTypeSelectList
        {
            get
            {
                if (_gTypes == null)
                    return new List<SelectListItem>();

                return _gTypes.Select(g => new SelectListItem()
                {
                    Value = g.Replace(" ",string.Empty),
                    Selected = this.AllowedGrantTypes != null && this.AllowedGrantTypes.Contains(g),
                    Text = g
                });
            }
        }

        public string ClientId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Name")]
        [Remote("UniqueClientName", "Clients", AdditionalFields = nameof(ClientId), ErrorMessage = "This client name is already taken please choose another name")]
        public string ClientName { get; set; }

        [DisplayName("Allowed Grant Types")]
        public string AllowedGrantTypes { get; set; }

        [DisplayName("Client Secret")]
        public string ClientSecret { get; set; }

        [DisplayName("Uri")]
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", ErrorMessage = "You have to provide a valid Uri")]
        public string Uri { get; set; }

        [DisplayName("Redirect Uri")]
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", ErrorMessage = "You have to provide a valid Uri")]
        public string RedirectUri { get; set; }

        [DisplayName("Logout Redirect Uri")]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", ErrorMessage = "You have to provide a valid Uri")]
        public string PostLogoutRedirectUri { get; set; }

        public IEnumerable<string> Resources { get; set; }

        [DisplayName("Allow Offline Access")]
        public bool AllowOfflineAccess { get; set; }

        [DisplayName("Refresh Token Expiration")]
        public TokenExpiration RefreshTokenExpiration { get; set; }

        [DisplayName("Refresh Token Usage")]
        public TokenUsage RefreshTokenUsage { get; set; }

        [DisplayName("Access Token Lifetime")]
        [Range(1, int.MaxValue, ErrorMessage = "Access Token Lifetime should be greater than zero and less than 2147483647")]
        public int AccessTokenLifetime { get; set; }

        [DisplayName("Identity Token Lifetime")]
        [Range(1, int.MaxValue, ErrorMessage = "Identity Token Lifetime should be greater than zero and less than 2147483647")]
        public int IdentityTokenLifetime { get; set; }

        [DisplayName("Require Consent")]
        public bool RequireConsent { get; set; }

        [DisplayName("Send Client Claims")]
        public bool AlwaysSendClientClaims { get; set; }


        [DisplayName("Allow Access Tokens Via Browser")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        [DisplayName("Allowed Cors Origin")]
        [Required(AllowEmptyStrings = false)]
        public string ClientAllowedCorsOrigin { get; set; }

        [DisplayName("Secret")]
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmationSecret),ErrorMessage = "Password incorrect")]
        public string Secret { get; set; }

        [DisplayName("C. Secret")]
        [DataType(DataType.Password)]
        public string ConfirmationSecret { get; set; }

        public IEnumerable<SelectListItem> ResourceList
        {
            get
            {

                if (_resources != null)
                {
                    return _resources.Select(s => new SelectListItem()
                    {
                        Value = s,
                        Text = s,
                        Selected = Resources?.Contains(s) ?? false
                    });
                }
                return new List<SelectListItem>();
            }
        }

        public IEnumerable<SelectListItem> TokenExpirationTypes => new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = TokenExpiration.Absolute.ToString(),
                Text = Enum.GetName(typeof(TokenExpiration),TokenExpiration.Absolute),
                Selected = this.RefreshTokenExpiration == TokenExpiration.Absolute
            },
            new SelectListItem()
            {
                Value = TokenExpiration.Sliding.ToString(),
                Text = Enum.GetName(typeof(TokenExpiration),TokenExpiration.Sliding),
                Selected = this.RefreshTokenExpiration == TokenExpiration.Sliding

            }
        };

        public IEnumerable<SelectListItem> TokenUsageTypes => new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = TokenUsage.OneTimeOnly.ToString(),
                Text = Enum.GetName(typeof(TokenUsage), TokenUsage.OneTimeOnly),
                Selected = this.RefreshTokenUsage == TokenUsage.OneTimeOnly
            },
            new SelectListItem()
            {
                Value = TokenUsage.ReUse.ToString(),
                Text = Enum.GetName(typeof(TokenUsage),TokenUsage.ReUse),
                Selected = this.RefreshTokenUsage == TokenUsage.ReUse
            }
        };


        public ClientEntity MapToModel()
        {
            var client = new ClientEntity();
            client.ClientId = this.ClientName.Replace(" ", "");
            client.ClientName = this.ClientName;
            client.ClientUri = this.Uri;
            client.AllowedGrantTypes = GetGrantTypes(this.AllowedGrantTypes);
            client.RedirectUris = new[] { this.RedirectUri };
            client.PostLogoutRedirectUris = new[] { this.PostLogoutRedirectUri };
            client.AllowedScopes = (ICollection<string>)this.Resources;
            client.AllowOfflineAccess = this.AllowOfflineAccess;
            client.RefreshTokenExpiration = this.RefreshTokenExpiration;
            client.RefreshTokenUsage = this.RefreshTokenUsage;
            client.AccessTokenLifetime = this.AccessTokenLifetime;
            client.IdentityTokenLifetime = this.IdentityTokenLifetime;
            client.RequireConsent = this.RequireConsent;
            client.AlwaysSendClientClaims = this.AlwaysSendClientClaims;
            client.Enabled = this.Enabled;
            client.AllowedCorsOrigins = (ICollection<string>)new[] { this.ClientAllowedCorsOrigin };
            client.ClientSecrets = new List<Secret>() { new Secret(this.Secret) };
            client.AllowAccessTokensViaBrowser = this.AllowAccessTokensViaBrowser;
            return client;
        }


        public void SetResources(IEnumerable<string> resources)
        {
            _resources = resources;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Resources == null || !this.Resources.Any())
                yield return new ValidationResult("You have to select a least one resource.", new[] { nameof(Resources) });

            if (this.AllowedGrantTypes == null || !this.AllowedGrantTypes.Any())
                yield return new ValidationResult("You have to select a least one grant type.", new[] { nameof(AllowedGrantTypes) });
        }

        public ICollection<string> GetGrantTypes(string grant)
        {
            switch (grant)
            {
                case nameof(GrantTypes.Code):
                    return GrantTypes.Code;
                case nameof(GrantTypes.Implicit):
                    return GrantTypes.Implicit;
                case nameof(GrantTypes.ResourceOwnerPasswordAndClientCredentials):
                    return GrantTypes.ResourceOwnerPasswordAndClientCredentials;
                case nameof(GrantTypes.ResourceOwnerPassword):
                    return GrantTypes.ResourceOwnerPassword;
                case nameof(GrantTypes.ClientCredentials):
                    return GrantTypes.ClientCredentials;
                case nameof(GrantTypes.HybridAndClientCredentials):
                    return GrantTypes.HybridAndClientCredentials;
                case nameof(GrantTypes.CodeAndClientCredentials):
                    return GrantTypes.CodeAndClientCredentials;
                case nameof(GrantTypes.ImplicitAndClientCredentials):
                    return GrantTypes.ImplicitAndClientCredentials;
                default:
                    throw new ArgumentException();
            }
        }

        private string SetAllowedGrandType(ICollection<string> clientAllowedGrantTypes)
        {
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.Code))
                return nameof(GrantTypes.Code);
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.Implicit))
                return nameof(GrantTypes.Implicit);
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.ResourceOwnerPasswordAndClientCredentials))
                return nameof(GrantTypes.ResourceOwnerPasswordAndClientCredentials);
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.ResourceOwnerPassword))
                return nameof(GrantTypes.ResourceOwnerPassword);
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.ClientCredentials))
                return nameof(GrantTypes.ClientCredentials);
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.HybridAndClientCredentials))
                return nameof(GrantTypes.HybridAndClientCredentials);
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.CodeAndClientCredentials))
                return nameof(GrantTypes.CodeAndClientCredentials);
            if (clientAllowedGrantTypes.SequenceEqual(GrantTypes.ImplicitAndClientCredentials))
                return nameof(GrantTypes.ImplicitAndClientCredentials);
            throw new ArgumentException(nameof(clientAllowedGrantTypes));
        }

    }

}

