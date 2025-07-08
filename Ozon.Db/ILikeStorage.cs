using Ozon.Db.Models;

namespace Ozon.Db
{
    public interface ILikeStorage
    {
        Task<bool> AddAsync(Guid userId, Product product);
        Task<bool> DeleteAsync(Guid userId, Product product);
        Task<bool> ClearAsync(Guid userId);
        Task<Like?> GetAsync(Guid userId);
    }
}