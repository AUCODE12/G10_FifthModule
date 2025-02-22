using ChatBot.Dal.Entites;
using ChatBot.Dal.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Dal;

public class MainContext : DbContext
{
    public DbSet<TelegramUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Data Source=localhost\\SQLDEV;User ID=sa;Password=1;Initial Catalog=MyBot;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
