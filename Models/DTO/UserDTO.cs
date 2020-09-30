using System;
using System.Linq.Expressions;

namespace IdentityServer.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual string Email { get; set; }

        public static Expression<Func<ApplicationUser, UserDTO>> Projection = p => new UserDTO
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email
        };
    }
}
