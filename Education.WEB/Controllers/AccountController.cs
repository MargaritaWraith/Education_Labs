using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.Entityes.EF.Identity;
using Education.WEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Education.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _SignInManager;

        public AccountController(SignInManager<User> SignInManager)
        {
            _SignInManager = SignInManager;
        }

        public IActionResult Login(string ReturnURL) => View(new LoginViewModel{ ReturnURL = ReturnURL } );

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var result = await _SignInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(login.ReturnURL))
                {
                   return Redirect(login.ReturnURL); 
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult AcessDenied()
        {
            return View();
        }

        public IActionResult Profile() => View();
    }
}