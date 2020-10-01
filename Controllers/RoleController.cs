using IdentityServer.Models;
using IdentityServer.Models.AccountViewModels;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        
        private readonly ILogger _logger;

        public RoleController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult New()
        {
            return View();
        }

        public async Task<IActionResult> UniqueRoleName(string Name, string Id)
        {
            try{
                if (string.IsNullOrEmpty(Id))
                {
                    var result = await _roleManager.FindByNameAsync(Name);
                    return Json(result == null);
                }

                return Json(true);
            }
            catch {
                //TODO need to add .WithError extension
                return RedirectToAction(nameof(Index));
            }
        }
    }
}