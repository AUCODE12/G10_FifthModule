using CarRentalSystem.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalSystem.Dal.EntityConfigurations;

public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.ToTable("CartProduct");

        builder.HasKey(b => new { b.CartId, b.ProductId });

        builder.HasOne(cp => cp.Product)
            .WithMany(p => p.CartProducts)
            .HasForeignKey(cp => cp.ProductId);

        builder.HasOne(cp => cp.Cart)
            .WithMany(c => c.CartProducts)
            .HasForeignKey(cp => cp.CartId);


    }
}
