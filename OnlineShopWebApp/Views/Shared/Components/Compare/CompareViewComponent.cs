using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Compare
{
    public class CompareViewComponent(ICompareStorage compareStorage, UserManager<User> usersManager) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var compare = await compareStorage.GetAsync(user?.Id ?? new());

            var compareCount = compare?.Products.Count ?? 0;

            return View("Compare", compareCount);
        }
    }
}
