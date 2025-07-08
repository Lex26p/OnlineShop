using Microsoft.EntityFrameworkCore;
using Ozon.Db.Models;

namespace Ozon.Db
{
    public class InDbCompareStorage(ApplicationDbContext applicationDbContext) : ICompareStorage
    {
        public async Task<Compare?> GetAsync(Guid userId)
        {
            return await applicationDbContext.Compares
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<bool> AddAsync(Guid userId, Product product)
        {
            var compare = await GetAsync(userId);

            if (compare != null)
            {
                if (compare.Products.Contains(product) == false)
                {
                    compare.Products.Add(product);

                    applicationDbContext.Update(compare);

                    await applicationDbContext.SaveChangesAsync();

                    return true;
                }
            }
            else
            {
                compare = new Compare()
                {
                    UserId = userId,
                    Products = [product]
                };

                applicationDbContext.Compares.Add(compare);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid userId, Product product)
        {
            var compare = await GetAsync(userId);

            if (compare != null)
            {
                compare.Products.Remove(product);

                applicationDbContext.Update(compare);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> ClearAsync(Guid userId)
        {
            var compare = await GetAsync(userId);

            if (compare != null)
            {
                compare.Products.Clear();

                applicationDbContext.Update(compare);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}