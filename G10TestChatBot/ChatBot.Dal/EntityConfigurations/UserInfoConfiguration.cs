using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Dal.EntityConfigurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable("UserInfo");
        builder.HasKey(ui => ui.UserInfoId);

        builder.Property(ui => ui.FirstName).IsRequired(false);
        builder.Property(ui => ui.LastName).IsRequired(false);
        builder.Property(ui => ui.Address).IsRequired(false);
        builder.Property(ui => ui.PhoneNumber).IsRequired(false);
        builder.Property(ui => ui.Email).IsRequired(false);
        builder.Property(ui => ui.Summary).IsRequired(false);


        builder.HasMany(ui => ui.Educations)
            .WithOne(ed => ed.UserInfo)
            .HasForeignKey(ed => ed.UserInfoId);

        builder.HasMany(ui => ui.Skills)
            .WithOne(s => s.UserInfo)
            .HasForeignKey(s => s.UserInfoId);

        builder.HasMany(ui => ui.Experiences)
            .WithOne(ex => ex.UserInfo)
            .HasForeignKey(ex => ex.UserInfoId);

        builder.HasMany(ui => ui.Projects)
            .WithOne(p => p.UserInfo)
            .HasForeignKey(p => p.UserInfoId);
    }
}
