using IdentityServer.Models;
using IdentityServer.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityServer.Controllers
{
    [Produces("application/json")]
    [Route("api/v1.0/Users")]
    [ApiVersion("1.0")]
    public class UserAPIController:ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserAPIController(ILogger<UserAPIController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;

        }

        // GET: api/v1.0/Users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(List<UserDTO>), 200)]
        public async Task<IActionResult> GetUsersAsync()
        {
            Task<IActionResult> task = Task<IActionResult>.Factory.StartNew(() =>
            {
                var users = _userManager.Users.Select(UserDTO.Projection).ToList();


                return Ok(users);
            });

            return await task;
        }

        // GET: api/v1.0/Users/Identifier/5
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User</returns>
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [HttpGet("Identifier/{id}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Id.ToLower() == id.ToLower());

            if (applicationUser == null)
            {
                return NoContent();
            }

            var user = new UserDTO
            {
                Id = applicationUser.Id,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email
            };

            return Ok(user);
        }
    }
}
