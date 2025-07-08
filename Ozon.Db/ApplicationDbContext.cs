
using Microsoft.EntityFrameworkCore;
using Ozon.Db.Models;

namespace Ozon.Db
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Compare> Compares { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product { Id = new Guid("3438f2c3-754d-4926-a8c5-ef89244b600c"), Name = "Вода", Cost = 120, Description = "Описание продукта" },
                    new Product { Id = new Guid("3cbf160f-7057-4342-8998-c807ab3bd17b"), Name = "Канат ", Cost = 137, Description = "Описание продукта" },
                    new Product { Id = new Guid("4e21a164-f1f5-4000-855a-8bb0fe6d1660"), Name = "Мыло", Cost = 334, Description = "Описание продукта" },
                    new Product { Id = new Guid("6e4446ac-a7b2-4e33-b9da-9b9528176826"), Name = "Ведро", Cost = 537, Description = "Описание продукта" },
                    new Product { Id = new Guid("c5d72e67-8122-418f-8e99-04510cabf5c8"), Name = "Корм", Cost = 623, Description = "Описание продукта" },
                    new Product { Id = new Guid("de472045-c37b-4ec4-a1b1-fe92b8949585"), Name = "Хлеб", Cost = 222, Description = "Описание продукта" }
            );
        }
    }
}