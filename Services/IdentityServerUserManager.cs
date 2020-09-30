using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class IdentityServerUserManager<T> : UserManager<ApplicationUser>
    {
        ApplicationDbContext _context;
        ClaimsIdentityOptions _options;
        public IdentityServerUserManager(ApplicationDbContext context, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
            _options = ((IdentityOptions)optionsAccessor.Value).ClaimsIdentity;
        }



        public ApplicationUser FindUserByProviderId(string providerId)
        {
            var user = Users.SingleOrDefault(u => u.UserName != null && u.UserName.Equals(providerId, StringComparison.InvariantCultureIgnoreCase));

            return user;
        }

        public Task<ApplicationUser> FindUserByProviderIdAsync(string providerId)
        {
            Task<ApplicationUser> task = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                var user = Users.SingleOrDefault(u => u.UserName != null && u.UserName.Equals(providerId, StringComparison.InvariantCultureIgnoreCase));
                return user;
            });

            return task;

        }

        public override Task<ApplicationUser> FindByIdAsync(string userId)
        {
            Task<ApplicationUser> task = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                var user = Users.SingleOrDefault(u => u.Id == userId);
                return user;
            });

            return task;

        }

        public override Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            Task<ApplicationUser> task = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                var id = principal.Claims.Single(c => c.Type == _options.UserIdClaimType);
                return Users.SingleOrDefault(u => u.Id == id.Value);
            });

            return task;
        }
    }
}
