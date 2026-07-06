using Ecom.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ProductId)
            .IsRequired();
    }
}