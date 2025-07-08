using Ozon.Db.Models;

namespace Ozon.Db
{
    public interface IRolesStorage
    {
        Task<bool> CheckNameAsync(Guid id, string name);
    }
}