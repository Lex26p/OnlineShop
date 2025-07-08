using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    public class LoginController(SignInManager<User> singInManager) : Controller
    {
        public IActionResult Index(string returnUrl)
        {
            return View(new LoginVM() { ReturnUrl = returnUrl });
        }

        public async Task<IActionResult> LogInAsync(LoginVM login)
        {

            if (ModelState.IsValid)
            {
                var result = await singInManager.PasswordSignInAsync(login.Email, login.Password, login.Remember, false);

                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl ?? "/Home");
                }
                ModelState.AddModelError("", "Неверный адрес электронной почты или пароль");
            }

            return View("Index", new LoginVM() { ReturnUrl = login.ReturnUrl });
        }

        public async Task<IActionResult> LogOutAsync()
        {
            await singInManager.SignOutAsync();

            return Redirect("~/Home/Index");
        }
    }
}