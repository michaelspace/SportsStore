using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AccountController: Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            IdentitySeedData.EnsurePopulated(userManager).Wait();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                if (User?.Identity?.Name != "Admin")
                {
                    return Redirect("/");
                }
                return Redirect("/Admin/Index");
            }

            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(loginModel.Name);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    ViewBag.LoginAdmin = false;

                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        ViewBag.LoginAdmin = true;
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Błędna nazwa użytkownika lub hasło.");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            ViewBag.LoginAdmin = false;
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public RedirectResult MainPage()
        {
            return Redirect("/");
        }
    }
}
