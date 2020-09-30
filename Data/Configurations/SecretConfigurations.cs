using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Configurations
{
    public class SecretConfigurations : IEntityTypeConfiguration<Secret>
    {
        public void Configure(EntityTypeBuilder<Secret> builder)
        {
            builder.HasKey(p => p.Description);
        }
    }
}
