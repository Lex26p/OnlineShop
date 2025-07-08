using Microsoft.AspNetCore.Identity;

namespace Ozon.Db.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Address { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public int OrdersCount { get; set; }
    }
}
