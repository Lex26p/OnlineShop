using Microsoft.EntityFrameworkCore;
using Ozon.Db.Models;

namespace Ozon.Db
{
    public class InDbLikeStorage(ApplicationDbContext applicationDbContext) : ILikeStorage
    {
        public async Task<Like?> GetAsync(Guid userId)
        {
            return await applicationDbContext.Likes
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<bool> AddAsync(Guid userId, Product product)
        {
            var like = await GetAsync(userId);

            if (like != null)
            {
                if (like.Products.Contains(product) == false)
                {
                    like.Products.Add(product);

                    applicationDbContext.Update(like);

                    await applicationDbContext.SaveChangesAsync();

                    return true;
                }
            }
            else
            {
                like = new Like()
                {
                    UserId = userId,
                    Products = [product]
                };

                applicationDbContext.Likes.Add(like);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid userId, Product product)
        {
            var like = await GetAsync(userId);

            if (like != null)
            {
                like.Products.Remove(product);

                applicationDbContext.Update(like);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> ClearAsync(Guid userId)
        {
            var like = await GetAsync(userId);

            if (like != null)
            {
                like.Products.Clear();

                applicationDbContext.Update(like);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}