using AuthCookies.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthCookies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Anonymous()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult UnAuthenticated()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Unauthorized()
        {
            return View();
        }

        public async Task<IActionResult> AddSimpleCookie() {
            
            Claim[] claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, "UserName"),
                new Claim(ClaimTypes.Role, "NormalUser")
            };
            var identities = new ClaimsIdentity(claims, "DefaultCookieSchema");
            var claimsPrincipal = new ClaimsPrincipal(identities);

            await HttpContext.SignInAsync(claimsPrincipal);            
            return Redirect("Authenticated");
        }

        [Authorize]
        public IActionResult Authenticated()
        {
            return View();
        }



        public async Task<IActionResult> AddSuperUserCookie()
        {

            Claim[] claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, "UserName"),
                new Claim(ClaimTypes.Role, "SuperUser")
            };
            var identities = new ClaimsIdentity(claims, "DefaultCookieSchema");
            var claimsPrincipal = new ClaimsPrincipal(identities);

            await HttpContext.SignInAsync(claimsPrincipal);
            return Redirect("Authenticated");
        }


        [Authorize(Roles = "SuperUser")]
        public IActionResult Authorized()
        {
            return View();
        }

        public async Task<IActionResult> RemoveCookies()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Anonymous");
        }
    }
}
