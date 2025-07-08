using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent(ICartsStorage cartsStorage, UserManager<User> usersManager) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var cartDB = await cartsStorage.GetAsync(user?.Id ?? new());

            if (cartDB != null)
            {
                var cart = new CartVM(cartDB);
                var productCount = cart?.Amount ?? 0;
                return View("Cart", productCount);
            }
            return View("Cart", 0);
        }
    }
}
