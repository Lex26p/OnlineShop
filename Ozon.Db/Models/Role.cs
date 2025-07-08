using Microsoft.AspNetCore.Identity;

namespace Ozon.Db.Models
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; } = "";

        public Role(string name)
        {
            Name = name;
        }

        public Role(string name, string description) : this(name)
        {
            Description = description;
        }
    }
}