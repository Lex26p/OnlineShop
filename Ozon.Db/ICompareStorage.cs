using Ozon.Db.Models;

namespace Ozon.Db
{
    public interface ICompareStorage
    {
        Task<Compare?> GetAsync(Guid userId);
        Task<bool> AddAsync(Guid userId, Product product);
        Task<bool> ClearAsync(Guid userId);
        Task<bool> DeleteAsync(Guid userId, Product product);
    }
}