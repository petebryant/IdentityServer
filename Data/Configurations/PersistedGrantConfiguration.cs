using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Configurations
{
    public class PersistedGrantConfiguration : IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> builder)
        {
            builder.HasKey(p => p.Key);
        }
    }
}
