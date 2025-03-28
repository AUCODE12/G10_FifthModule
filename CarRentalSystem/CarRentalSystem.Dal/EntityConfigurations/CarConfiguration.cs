using CarRentalSystem.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Dal.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Car");

        builder.HasKey(c => c.CarId);

        builder.Property(c => c.Model)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(c => c.Brand)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(c => c.CreatedYear)
            .IsRequired(true);

        builder.Property(c => c.PricePerDay)
            .IsRequired(true);
           
        builder.HasMany(c => c.Bookings)
            .WithOne(b => b.Car)
            .HasForeignKey(b => b.CarId);

        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Car)
            .HasForeignKey(r => r.CarId);
    }
}

