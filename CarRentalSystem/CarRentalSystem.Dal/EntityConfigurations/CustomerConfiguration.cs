using CarRentalSystem.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Dal.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(c => c.CustomerId);

        builder.Property(c => c.FirstName)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.HasIndex(c => c.Email).IsUnique(true);

        builder.Property(c => c.PhoneNumber)
            .IsRequired(true)
            .HasMaxLength(13);

        builder.HasMany(c => c.Bookings)
            .WithOne(b => b.Customer)
            .HasForeignKey(b => b.CustomerId);

        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId);
    }
}
