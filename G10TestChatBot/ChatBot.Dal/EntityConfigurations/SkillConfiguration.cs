using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Dal.EntityConfigurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skill");

        builder.HasKey(s => s.SkillId);

        builder.Property(s => s.Name).IsRequired(false);
        builder.Property(s => s.ProficiencyLevel).IsRequired(false);
    }
}
