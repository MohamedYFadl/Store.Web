using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System.Reflection;

namespace Store.Data.Contexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        DbSet<ProductBrand> ProductBrands { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductType> ProductTypes { get; set; }
    }
}
