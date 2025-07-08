using Microsoft.EntityFrameworkCore;
using Ozon.Db.Models;

namespace Ozon.Db
{
    public class InDbOrdersStorage(ApplicationDbContext applicationDbContext) : IOrdersStorage
    {
        public async Task<bool> AddAsync(Order order)
        {
            var cart = await applicationDbContext.Carts
                .Include(p => p.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(p => p.UserId == order.UserId);
            if (cart != null)
            {
                order.Cart.Items.AddRange(cart.Items);

                applicationDbContext.Orders.Add(order);

                await applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await applicationDbContext.Orders
                .AsNoTracking()
                .Include(p => p.Cart)
                .ThenInclude(p => p.Items)
                .ThenInclude(p => p.Product)
                .ToListAsync() ?? [];
        }

        public async Task<List<Order>> UserGetAllAsync(Guid id)
        {
            var orders = await GetAllAsync();
            return orders.Where(p => p.UserId == id).ToList() ?? [];
        }

        public async Task<Order?> GetAsync(Guid id)
        {
            return await applicationDbContext.Orders
                .Include(p => p.Cart)
                .ThenInclude(p => p.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ChangeStatusAsync(Guid id, OrderStatus status)
        {
            var order = await applicationDbContext.Orders.FirstOrDefaultAsync(p => p.Id == id);
            if (order != null)
            {
                order.Status = status;

                await applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}