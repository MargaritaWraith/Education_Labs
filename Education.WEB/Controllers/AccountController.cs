using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.Entityes.EF.Identity;
using Education.WEB.Models;
using Education.WEB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Education.WEB.Controllers
{
    public class AccountController : Controller
    {
        #region Поля

        private readonly SignInManager<User> _SignInManager;

        #endregion

        #region Конструктор

        public AccountController(SignInManager<User> SignInManager) => _SignInManager = SignInManager;

        #endregion

        #region Действия

        #region Login

        public IActionResult Login(string ReturnURL) => View(new LoginViewModel { ReturnURL = ReturnURL });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var result = await _SignInManager.PasswordSignInAsync(
                login.UserName, login.Password,
                login.RememberMe, false);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(login.ReturnURL))
                    return Redirect(login.ReturnURL);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(login);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcessDenied() => View();

        public IActionResult Profile() => View();

        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, [FromServices] UserManager<User> manager)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                UserName = model.UserName,
                Surname = model.Surname,
                Name = model.Name,
                Patronimic = model.Patronymic,
                Email = model.Email
            };
            var CreationResult = await manager.CreateAsync(user, model.Password);

            if (CreationResult.Succeeded)
            {
                await _SignInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in CreationResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        #endregion
    }
}