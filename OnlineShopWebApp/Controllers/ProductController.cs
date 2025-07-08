using Microsoft.AspNetCore.Mvc;
using Ozon.Db;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController(IProductsStorage productsStorage) : Controller
    {
        public async Task<IActionResult> Index(Guid id)
        {
            var product = await productsStorage.GetAsync(id);

            return View(product);
        }
    }
}