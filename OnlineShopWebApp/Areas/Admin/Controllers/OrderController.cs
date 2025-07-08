using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Consts.AdminRoleName)]
    [Authorize(Roles = Consts.AdminRoleName)]
    public class OrderController(IOrdersStorage ordersStorage) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var ordersDB = await ordersStorage.GetAllAsync();
            List<OrderVM> orders = [];
            if (ordersDB != null)
            {
                foreach (Order p in ordersDB)
                {
                    orders.Add(new OrderVM(p));
                }
            }
            return View(orders);
        }

        public async Task<IActionResult> OrderAsync(Guid id)
        {
            var orderDB = await ordersStorage.GetAsync(id);
            if (orderDB != null)
            {
                var order = new OrderVM(orderDB);
                return View(order);
            }

            return View("Index");
        }

        public async Task<IActionResult> ChangeStatusAsync(Guid id, OrderStatus status)
        {
            await ordersStorage.ChangeStatusAsync(id, status);

            return Redirect($"~/Admin/Order/Order/{id}");
        }
    }
}