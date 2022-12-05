using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5Cross.Models
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Challenge(OktaDefaults.MvcAuthenticationScheme);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult SignOut()
        {
            return new SignOutResult(
                new[]
                {
                    OktaDefaults.MvcAuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                },
                new AuthenticationProperties { RedirectUri = "/Home/" });
        }
        [HttpGet]
        public IActionResult Profile()
        {
            return View(new UserProfileModel()
            {
                Email = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault().Value.ToString(),
                FirstName = HttpContext.User.Claims.Where(x => x.Type == "given_name").FirstOrDefault().Value.ToString(),
                LastName = HttpContext.User.Claims.Where(x => x.Type == "family_name").FirstOrDefault().Value.ToString(),
                UserName = HttpContext.User.Claims.Where(x => x.Type == "preferred_username").FirstOrDefault().Value.ToString(),
            });
        }
    }
}
