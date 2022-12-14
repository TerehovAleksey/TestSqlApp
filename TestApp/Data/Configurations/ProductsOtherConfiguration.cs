using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Models;

namespace TestApp.Data.Configurations
{
    public class ProductsOtherConfiguration : IEntityTypeConfiguration<ProductOther>
    {
        public void Configure(EntityTypeBuilder<ProductOther> builder)
        {
            builder.ToTable("Tab1")
                .HasKey(x => x.Id);

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
