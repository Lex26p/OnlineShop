using Microsoft.EntityFrameworkCore;
using Ozon.Db.Models;

namespace Ozon.Db
{
    public class InDbCartsStorage(ApplicationDbContext applicationDbContext) : ICartsStorage
    {
        public async Task<Cart?> GetAsync(Guid userId)
        {
            return await applicationDbContext.Carts
                .Include(p => p.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<bool> AddAsync(Guid userId, Product product)
        {
            var cart = await GetAsync(userId);

            if (cart != null)
            {
                var items = cart.Items.FirstOrDefault(i => i.Product.Id == product.Id);
                if (items != null)
                {
                    items.Count++;
                }
                else
                {
                    var cartItem = new CartItem()
                    {
                        Product = product,
                        Count = 1
                    };
                    cart.Items.Add(cartItem);
                }
                applicationDbContext.Update(cart);
            }
            else
            {
                var cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    Product = product,
                    Count = 1
                };
                cart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = [cartItem]
                };
                applicationDbContext.Carts.Add(cart);
            }
            await applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReduceAsync(Guid userId, Guid productId)
        {
            var cart = await GetAsync(userId);

            if (cart != null)
            {
                var items = cart.Items.FirstOrDefault(i => i.Product.Id == productId);
                if (items != null)
                {
                    if (items.Count > 1)
                    {
                        items.Count--;
                    }
                    else
                    {
                        cart.Items.Remove(items);
                    }
                }
                applicationDbContext.Update(cart);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> ClearAsync(Guid userId)
        {
            var cart = await GetAsync(userId);

            if (cart != null)
            {
                applicationDbContext.Remove(cart);

                await applicationDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}