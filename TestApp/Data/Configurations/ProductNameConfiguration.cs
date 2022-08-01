using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Models;

namespace TestApp.Data.Configurations
{
    public class ProductNameConfiguration : IEntityTypeConfiguration<ProductName>
    {
        public void Configure(EntityTypeBuilder<ProductName> builder)
        {
            builder.ToTable("Names")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250);
        }
    }
}
