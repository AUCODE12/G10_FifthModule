using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Dal.EntityConfigurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Education");
        builder.HasKey(e => e.EducationId);

        builder.Property(e => e.Institution).IsRequired(false);
        builder.Property(e => e.Degree).IsRequired(false);
        builder.Property(e => e.StartDate).IsRequired(false);
        builder.Property(e => e.EndDate).IsRequired(false);
    }
}
