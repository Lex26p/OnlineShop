using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Consts.AdminRoleName)]
    [Authorize(Roles = Consts.AdminRoleName)]
    public class UsersController(IMapper mapper, UserManager<User> usersManager, RoleManager<Role> rolesManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await usersManager.Users.ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> UserInfoAsync(Guid id)
        {
            var user = await usersManager.FindByIdAsync(id.ToString());

            return View(user);
        }

        public IActionResult EditPassword(Guid id)
        {
            var password = new EditPasswordVM()
            {
                Id = id
            };
            return View(password);
        }

        [HttpPost]
        public async Task<IActionResult> EditPasswordAsync(Guid id, EditPasswordVM editPassword)
        {
            var user = await usersManager.FindByIdAsync(id.ToString());

            if (editPassword != null && ModelState.IsValid && user != null)
            {
                user.PasswordHash = usersManager.PasswordHasher.HashPassword(user, editPassword.Password);
                var result = await usersManager.UpdateAsync(user);

                if (result != null && result.Succeeded)
                {
                    return Redirect($"~/Admin/Users/UserInfo/{id}");
                }
            }
            ModelState.AddModelError("", "Ошибка введенных данных, проверьте и попробуйте еще раз");
            var password = new EditPasswordVM()
            {
                Id = id
            };

            return View("EditPassword", password);
        }

        public async Task<IActionResult> EditUserAsync(Guid id)
        {
            var user = await usersManager.FindByIdAsync(id.ToString());

            return View(new EditUserVM(user ?? new()));
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAsync(Guid id, EditUserVM user)
        {
            var userDB = await usersManager.FindByIdAsync(id.ToString());

            if (ModelState.IsValid && userDB != null && user != null)
            {
                mapper.Map<EditUserVM, User>(user, userDB);

                var result = await usersManager.UpdateAsync(userDB);

                if (result != null && result.Succeeded)
                {
                    return Redirect($"~/Admin/Users/UserInfo/{id}");
                }
            }
            ModelState.AddModelError("", "Ошибка введенных данных, проверьте и попробуйте еще раз");
            return View("EditUser", new EditUserVM(userDB ?? new()));
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await usersManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                await usersManager.DeleteAsync(user);
            }

            return Redirect("~/Admin/Users/Index");
        }

        public async Task<IActionResult> UserRolesAsync(Guid id)
        {
            var user = await usersManager.FindByIdAsync(id.ToString());

            var roles = await rolesManager.Roles.ToListAsync();

            var userRolesVM = mapper.Map<UserRolesVM>(user);

            if (user != null && roles != null)
            {
                var UserRoles = await usersManager.GetRolesAsync(user) ?? [];

                foreach (Role role in roles)
                {
                    if (UserRoles.Contains(role.Name ?? ""))
                    {
                        userRolesVM.Roles.Add(role);
                    }
                    else
                    {
                        userRolesVM.RolesDB.Add(role);
                    }
                }
            }

            return View(userRolesVM);
        }

        public async Task<IActionResult> AddRoleAsync(Guid id, string roleName)
        {
            var user = await usersManager.FindByIdAsync(id.ToString());

            if (await rolesManager.RoleExistsAsync(roleName) && user != null)
            {
                await usersManager.AddToRoleAsync(user, roleName);
            }

            return Redirect("~/Admin/Users/UserRoles/" + id);
        }

        public async Task<IActionResult> DeleteRoleAsync(Guid id, string roleName)
        {
            var user = await usersManager.FindByIdAsync(id.ToString());

            if (await rolesManager.RoleExistsAsync(roleName) && user != null)
            {
                await usersManager.RemoveFromRoleAsync(user, roleName);
            }

            return Redirect("~/Admin/Users/UserRoles/" + id);
        }
    }
}