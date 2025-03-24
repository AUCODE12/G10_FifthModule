using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCRUD.DataAccess.Entity;

namespace MusicCRUD.DataAccess.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserName).
            IsRequired(true).
            HasMaxLength(30);

        builder.Property(u => u.FirstName)
            .IsRequired(true)
            .HasMaxLength(30);

        builder.Property(u => u.LastName).
            IsRequired(false).
            HasMaxLength(20);

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.UserName).IsUnique();

        builder.HasOne(u => u.UserDetail)
            .WithOne(uD => uD.User)
            .HasForeignKey<UserDetail>(u => u.UserId);
    }
}