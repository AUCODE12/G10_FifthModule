using ChatBot.Dal.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatBot.Dal.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<TelegramUser>
{
    public void Configure(EntityTypeBuilder<TelegramUser> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.BotUserId);

        builder.HasIndex(u => u.TelegramUserId).IsUnique(true); 
    }
}
