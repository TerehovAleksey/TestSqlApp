using Microsoft.EntityFrameworkCore;
using TestApp.Data.Configurations;
using TestApp.Models;

namespace TestApp.Data
{
    public class AppDb2Context : DbContext
    {
        public AppDb2Context(DbContextOptions<AppDb2Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsNotInDb1Configuration());
            modelBuilder.ApplyConfiguration(new ProductsOtherConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductNotInDb1> ProductsNotInDb1 => Set<ProductNotInDb1>();
        public DbSet<ProductOther> ProductsOther => Set<ProductOther>();
    }
}
