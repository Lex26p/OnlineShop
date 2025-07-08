using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    public class UserController(SignInManager<User> singInManager, IMapper mapper, UserManager<User> usersManager, RoleManager<Role> roleManager, IImagesService imagesService) : Controller
    {
        public IActionResult Index(string returnUrl)
        {
            return View(new NewUserVM() { ReturnUrl = returnUrl });
        }
        public async Task<IActionResult> CreateAsync(NewUserVM newUser)
        {
            if (ModelState.IsValid)
            {
                if (await usersManager.FindByNameAsync(newUser.Email) == null)
                {
                    var user = mapper.Map<User>(newUser);

                    if (newUser.FormImage != null)
                    {
                        user.ImagePath = (await imagesService.AddAsync(Consts.UserImageFolder, [newUser.FormImage]))[0];
                    }

                    if (await roleManager.FindByNameAsync(Consts.UserRoleName) == null)
                    {
                        await roleManager.CreateAsync(new Role(Consts.UserRoleName));
                    }

                    var result =  await usersManager.CreateAsync(user, newUser.Password);

                    if (result != null && result.Succeeded)
                    {
                        await usersManager.AddToRoleAsync(user, Consts.UserRoleName);
                    }

                    return Redirect(newUser.ReturnUrl ?? "/Home");
                }

                ModelState.AddModelError("Email", "Пользователь с таким адресом уже существует");

                return View("Index", new NewUserVM() { ReturnUrl = newUser.ReturnUrl });
            }

            ModelState.AddModelError("", "Ошибка введенных данных, проверьте и попробуйте еще раз");

            return View("Index");
        }

        [Authorize]
        public async Task<IActionResult> UserInfoAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            return View(user);
        }

        [Authorize]
        public IActionResult EditPassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPasswordAsync(EditPasswordVM editPassword)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (editPassword != null && ModelState.IsValid && user != null)
            {
                user.PasswordHash = usersManager.PasswordHasher.HashPassword(user, editPassword.Password);

                var result = await usersManager.UpdateAsync(user);

                if (result != null && result.Succeeded)
                {
                    return Redirect($"~/User/UserInfo");
                }
            }

            ModelState.AddModelError("", "Ошибка введенных данных, проверьте и попробуйте еще раз");

            return View("EditPassword");
        }

        [Authorize]
        public async Task<IActionResult> EditUserAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            return View(new EditUserVM(user ?? new()));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditUserAsync(EditUserVM user)
        {
            var userDB = await usersManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid && userDB != null && user != null)
            {
                var userId = userDB.Id;

                mapper.Map<EditUserVM, User>(user, userDB);

                userDB.Id = userId;

                var result = await usersManager.UpdateAsync(userDB);

                if (result != null && result.Succeeded)
                {
                    return Redirect($"~/User/UserInfo");
                }
            }
            ModelState.AddModelError("", "Ошибка введенных данных, проверьте и попробуйте еще раз");
            return View("EditUser", new EditUserVM(userDB ?? new()));
        }

        [Authorize]
        public async Task<IActionResult> DeleteAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                await singInManager.SignOutAsync();

                await usersManager.DeleteAsync(user);
            }

            return Redirect("~/Home/Index");
        }

        [Authorize]
        public async Task<IActionResult> EditAvatarAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            return View(user ?? new());

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAvatarAsync(IFormFile formImage)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (user != null && formImage != null)
            {
                user.ImagePath = await imagesService.EditAsync(Consts.UserImageFolder, formImage, user.ImagePath);

                var result = await usersManager.UpdateAsync(user);

                if (result != null && result.Succeeded)
                {
                    return Redirect($"~/User/UserInfo");
                }
            }
            return View("Error");
        }

        [Authorize]
        public async Task<IActionResult> DeleteAvatarAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (user != null && user.ImagePath != "")
            {
                imagesService.Delete(user.ImagePath);

                user.ImagePath = "";

                var result = await usersManager.UpdateAsync(user);

                if (result != null && result.Succeeded)
                {
                    return Redirect($"~/User/UserInfo");
                }
            }

            return Redirect($"~/User/EditAvatar");
        }

    }
}