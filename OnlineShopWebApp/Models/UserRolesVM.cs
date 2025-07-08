using Ozon.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class UserRolesVM
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public List<Role> Roles { get; set; } = [];
        public List<Role> RolesDB { get; set; } = [];
        public string UserName { get; set; } = "";
    }
}