using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController(IMapper mapper, ICartsStorage cartsStorage, IOrdersStorage ordersStorage, UserManager<User> usersManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> BuyAsync(OrderInputVM orderVM)
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid && orderVM != null && user != null)
            {
                var orderDB = mapper.Map<Order>(orderVM);
                orderDB.UserId = user.Id;

                await ordersStorage.AddAsync(orderDB);

                await cartsStorage.ClearAsync(user.Id);

                return View(orderDB);
            }

            return View("Index");
        }

        public async Task<IActionResult> UserOrdersAsync()
        {
            var user = await usersManager.GetUserAsync(HttpContext.User);

            var ordersDB = await ordersStorage.UserGetAllAsync(user?.Id ?? new());

            List<OrderVM> ordersVM = [];

            if (ordersDB != null)
            {
                foreach (Order p in ordersDB)
                {
                    ordersVM.Add(new OrderVM(p));
                }
            }
            return View(ordersVM);
        }

        public async Task<IActionResult> OrderInfoAsync(Guid id)
        {
            var orderDB = await ordersStorage.GetAsync(id);
            if (orderDB != null)
            {
                var order = new OrderVM(orderDB);
                return View(order);
            }

            return View("Index");
        }
    }
}