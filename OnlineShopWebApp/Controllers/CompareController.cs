using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CompareController(ICompareStorage compareStorage, IProductsStorage productsStorage, UserManager<User> usersManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var compare = await compareStorage.GetAsync(user?.Id ?? new());

            return View(compare);
        }

        public async Task<IActionResult> AddAsync(Guid id)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var product = await productsStorage.GetAsync(id);

            if (product != null && user != null)
            {
                await compareStorage.AddAsync(user.Id, product);
            }
            return Redirect("~/Compare/Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var product = await productsStorage.GetAsync(id);

            if (product != null && user != null)
            {
                await compareStorage.DeleteAsync(user.Id, product);
            }
            return Redirect("~/Compare/Index");
        }

        public async Task<IActionResult> ClearAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            await compareStorage.ClearAsync(user?.Id ?? new());

            return Redirect("~/Compare/Index");
        }
    }
}
