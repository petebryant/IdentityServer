using IdentityServer.Data.Configurations;
using IdentityServer.Models;
using IdentityServer.Models.IdentityServer;
using IdentityServer4.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PersistedGrantConfiguration());
            builder.ApplyConfiguration(new SecretConfigurations());
            builder.ApplyConfiguration(new ClientEntityConfigurations());
        }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
    }
}
