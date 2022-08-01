using Microsoft.EntityFrameworkCore;
using TestApp.Data.Configurations;
using TestApp.Models;

namespace TestApp.Data
{
    public class AppDb1Context : DbContext
    {
        public AppDb1Context(DbContextOptions<AppDb1Context> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductNameConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAmountConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPriceConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductName> Names => Set<ProductName>();
        public DbSet<ProductPrice> Prices => Set<ProductPrice>();
        public DbSet<ProductCount> Amounts => Set<ProductCount>();
    }
}
