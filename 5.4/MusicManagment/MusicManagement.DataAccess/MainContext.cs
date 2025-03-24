using Microsoft.EntityFrameworkCore;
using MusicManagement.DataAccess.Entities;

namespace MusicManagement.DataAccess;

public class MainContext : DbContext
{
    public DbSet<Music> Music { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;User ID=sa;Password=1;Initial Catalog=MusicManagment;TrustServerCertificate=True;");
        }
    }

    //public MainContext(DbContextOptions<MainContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
