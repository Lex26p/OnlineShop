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
    public class RoleController(RoleManager<Role> rolesManager, IRolesStorage rolesStorage) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var roles = await rolesManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(RoleVM role)
        {
            if (ModelState.IsValid)
            {
                if (await rolesManager.RoleExistsAsync(role.Name))
                {
                    ModelState.AddModelError("Name", "Роль с таким названием уже существует");

                    return View();
                }
                await rolesManager.CreateAsync(new Role(role.Name, role.Description));
            }

            return Redirect("~/Admin/Role/Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var role = await rolesManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                await rolesManager.DeleteAsync(role);
            }
            return Redirect("~/Admin/Role/Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var role = await rolesManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                return View(new RoleVM(role));
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Guid id, RoleVM role)
        {
            if (role != null && ModelState.IsValid)
            {
                var _role = await rolesManager.FindByIdAsync(id.ToString());

                if (await rolesStorage.CheckNameAsync(id, role.Name))
                {
                    ModelState.AddModelError("Name", "Роль с таким названием уже существует");

                    if (_role != null)
                    {
                        return View(new RoleVM(_role));
                    }
                    return View("Error");
                }

                if (_role != null)
                {
                    _role.Name = role.Name.Trim();
                    _role.Description = role.Description;
                    await rolesManager.UpdateAsync(_role);
                }
            }

            return Redirect("~/Admin/Role/Index");
        }
    }
}