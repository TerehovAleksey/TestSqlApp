using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Models;

namespace TestApp.Data.Configurations
{
    public class ProductAmountConfiguration : IEntityTypeConfiguration<ProductCount>
    {
        public void Configure(EntityTypeBuilder<ProductCount> builder)
        {
            builder.ToTable("Amounts")
                .HasKey(x => x.Id);
            
            builder.Property(x => x.Article)
                .IsRequired();

            builder.Property(x => x.Quantity)
            .IsRequired();
        }
    }
}
