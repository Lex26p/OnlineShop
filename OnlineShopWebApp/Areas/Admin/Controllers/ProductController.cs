using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using Ozon.Db;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Consts.AdminRoleName)]
    [Authorize(Roles = Consts.AdminRoleName)]
    public class ProductController(IMapper mapper, IProductsStorage productsStorage, IImagesService imagesService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await productsStorage.GetAllAsync();

            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductVM product)
        {
            var productDB = mapper.Map<Product>(product);

            if (product.FormImage.Count > 0)
            {
                var imagePaths = await imagesService.AddAsync(Consts.ProductImageFolder, product.FormImage);

                if (imagePaths.Count > 0)
                {
                    productDB.ImagesPath.AddRange(imagePaths);
                }
            }

            await productsStorage.AddAsync(productDB);

            return Redirect("~/Admin/Product/Index");
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var product = await productsStorage.GetAsync(id);

            if (product != null)
            {
                var productVM = mapper.Map<ProductVM>(product);

                return View(productVM);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductVM product)
        {
            var productDB = mapper.Map<Product>(product);

            await productsStorage.EditAsync(productDB);

            return Redirect("~/Admin/Product/Index");
        }

        public async Task<IActionResult> EditImageAsync(Guid id)
        {
            var product = await productsStorage.GetAsync(id);

            if (product != null)
            {
                var productVM = mapper.Map<ProductVM>(product);

                return View(productVM);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditImageAsync(Guid id, List<IFormFile> formImages)
        {
            if (formImages != null && formImages.Count > 0)
            {
                var imagePaths = await imagesService.AddAsync(Consts.ProductImageFolder, formImages);

                if (imagePaths.Count > 0)
                {
                    await productsStorage.EditImagesAsync(id, imagePaths);
                }
            }
            return Redirect("~/Admin/Product/EditImage/" + id);
        }

        public async Task<IActionResult> DeleteImageAsync(Guid id, string path)
        {
            if (path != null)
            {
                imagesService.Delete(path);

                await productsStorage.DeleteImageAsync(id, path);

                return Redirect("~/Admin/Product/EditImage/" + id);
            }

            return View("Error");
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await productsStorage.DeleteAsync(id);

            return Redirect("~/Admin/Product/Index");
        }
    }
}