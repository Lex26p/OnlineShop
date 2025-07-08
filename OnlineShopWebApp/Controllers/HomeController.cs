using Microsoft.AspNetCore.Mvc;
using Ozon.Db;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController(IProductsStorage productsStorage) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await productsStorage.GetAllAsync());
        }

        public async Task<IActionResult> SearchAsync(string request)
        {
            return View("Index", await productsStorage.SearchAsync(request));
        }
    }
}