using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Models;

namespace TestApp.Data.Configurations
{
    public class ProductsNotInDb1Configuration : IEntityTypeConfiguration<ProductNotInDb1>
    {
        public void Configure(EntityTypeBuilder<ProductNotInDb1> builder)
        {
            builder.ToTable("Tab2")
                .HasAlternateKey(x => x.Id);

            builder.Property(x => x.Article)
                .IsRequired();
            
            builder.Property(x => x.Name)
                .IsRequired();
            
            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("REAL");
        }
    }
}
