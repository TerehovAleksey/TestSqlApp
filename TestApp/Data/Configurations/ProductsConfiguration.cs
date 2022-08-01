using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Models;

namespace TestApp.Data.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("MONEY");
        }
    }
}
