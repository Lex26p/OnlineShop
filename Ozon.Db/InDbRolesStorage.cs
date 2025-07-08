using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ozon.Db.Models;
using System.Data;

namespace Ozon.Db
{
    public class InDbRolesStorage(RoleManager<Role> rolesManager) : IRolesStorage
    {
        public async Task<bool> CheckNameAsync(Guid id, string name)
        {
            var roles = await rolesManager.Roles.AsNoTracking().ToListAsync();
            return roles.Where(p => p.Id != id)
                .Any(p => (p.Name ?? "")
                .Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));        }
    }
}