using IdentityServer.Models.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Configurations
{
    public class ClientEntityConfigurations : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.HasKey(p => p.ClientId);
            builder.Ignore(p => p.Claims);
            builder.Ignore(p => p.AllowedGrantTypes);
            builder.Ignore(p => p.RedirectUris);
            builder.Ignore(p => p.PostLogoutRedirectUris);
            builder.Ignore(p => p.AllowedScopes);
            builder.Ignore(p => p.IdentityProviderRestrictions);
            builder.Ignore(p => p.AllowedCorsOrigins);
            builder.Ignore(p => p.Properties);
            builder.Ignore(p => p.ClientSecrets);
        }
    }
}
