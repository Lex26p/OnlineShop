using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class LikeController(ILikeStorage likeStorage, IProductsStorage productsStorage, UserManager<User> usersManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var like = await likeStorage.GetAsync(user?.Id ?? new());

            return View(like);
        }

        public async Task<IActionResult> AddAsync(Guid id)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var product = await productsStorage.GetAsync(id);

            if (product != null && user != null)
            {
                await likeStorage.AddAsync(user.Id, product);
            }

            return Redirect("~/Like/Index");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var product = await productsStorage.GetAsync(id);

            if (product != null && user != null)
            {
                await likeStorage.DeleteAsync(user.Id, product);
            }
            return Redirect("~/Like/Index");
        }

        public async Task<IActionResult> ClearAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                await likeStorage.ClearAsync(user.Id);
            }

            return Redirect("~/Like/Index");
        }
    }
}