using Microsoft.EntityFrameworkCore;
using TestApp.Data.Configurations;
using TestApp.Models;

namespace TestApp.Data
{
    public class AppDb2Context : DbContext
    {
        public AppDb2Context(DbContextOptions<AppDb2Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products => Set<Product>();
    }
}
