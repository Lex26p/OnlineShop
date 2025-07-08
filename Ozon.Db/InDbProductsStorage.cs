using Microsoft.EntityFrameworkCore;
using Ozon.Db.Models;

namespace Ozon.Db
{
    public class InDbProductsStorage(ApplicationDbContext applicationDbContext) : IProductsStorage
    {
        public async Task<Product?> GetAsync(Guid id)
        {
            return await applicationDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await applicationDbContext.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Product>> SearchAsync(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var products = await applicationDbContext.Products.ToListAsync();

                return products.Where(p => p.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            return [];
        }

        public async Task<bool> AddAsync(Product product)
        {
            if (product != null)
            {
                await applicationDbContext.Products.AddAsync(product);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> EditAsync(Product productDb)
        {
            var product = await GetAsync(productDb.Id);

            if (product != null && productDb != null)
            {
                product.Name = productDb.Name;
                product.Cost = productDb.Cost;
                product.Description = productDb.Description;

                applicationDbContext.Update(product);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await GetAsync(id);

            if (product != null)
            {
                applicationDbContext.Products.Remove(product);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteImageAsync(Guid id, string path)
        {
            var product = await GetAsync(id);

            if (product != null)
            {
                product.ImagesPath.Remove(path);

                applicationDbContext.Update(product);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> EditImagesAsync(Guid id, List<string> images)
        {
            var product = await GetAsync(id);

            if (product != null && images != null && images.Count > 0)
            {
                product.ImagesPath.AddRange(images);

                applicationDbContext.Update(product);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}