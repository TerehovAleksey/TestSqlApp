using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Models;

namespace TestApp.Data.Configurations
{
    public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("Prices")
                .HasKey(x => x.Id);
            
            builder.Property(x => x.Article)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("REAL");
        }
    }
}
