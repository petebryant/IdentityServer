using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityServer.Models;
using IdentityServer.Models.SecureViewModels;
using IdentityServer.Services;

namespace IdentityServer.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class SecureController : Controller
    {
        private readonly ILogger<SecureController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public SecureController(UserManager<ApplicationUser> userManager, ILogger<SecureController> logger)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
            };

            return View(model);
        }
    }
}