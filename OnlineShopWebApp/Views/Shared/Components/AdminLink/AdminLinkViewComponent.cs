using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.AdminLink
{
    public class AdminLinkViewComponent(UserManager<User> usersManager) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            return View("AdminLink", user?.ImagePath ?? "");
        }
    }
}
