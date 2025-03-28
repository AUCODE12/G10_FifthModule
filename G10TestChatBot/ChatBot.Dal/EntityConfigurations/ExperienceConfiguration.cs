using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Dal.EntityConfigurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experience");

        builder.HasKey(e => e.ExperienceId);

        builder.Property(e => e.Company).IsRequired(false);
        builder.Property(e => e.Position).IsRequired(false);
        builder.Property(e => e.StartDate).IsRequired(false);
        builder.Property(e => e.EndDate).IsRequired(false);
        builder.Property(e => e.Description).IsRequired(false);
    }
}
