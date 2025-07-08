using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController(ICartsStorage cartsStorage, IProductsStorage productsStorage, UserManager<User> usersManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var cartDB = await cartsStorage.GetAsync(user?.Id ?? new());

            if (cartDB != null)
            {
                var cart = new CartVM(cartDB);
                return View(cart);
            }
            return View(null);
        }

        public async Task<IActionResult> AddAsync(Guid id)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var product = await productsStorage.GetAsync(id);

            if (product != null && user != null)
            {
                await cartsStorage.AddAsync(user.Id, product);
            }
            return Redirect("~/Cart/Index");
        }

        public async Task<IActionResult> ReduceAsync(Guid id)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var product = await productsStorage.GetAsync(id);

            if (product != null && user != null)
            {
                await cartsStorage.ReduceAsync(user.Id, product.Id);
            }
            return Redirect("~/Cart/Index");
        }

        public async Task<IActionResult> ClearAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            await cartsStorage.ClearAsync(user?.Id ?? new());

            return Redirect("~/Cart/Index");
        }
    }
}