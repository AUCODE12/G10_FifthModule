using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Dal.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");

        builder.HasKey(p => p.ProjectId);

        builder.Property(p => p.Name).IsRequired(false);
        builder.Property(p => p.Description).IsRequired(false);
        builder.Property(p => p.StartingTime).IsRequired(false);
        builder.Property(p => p.EndingTime).IsRequired(false);
    }
}
