using Microsoft.EntityFrameworkCore;
using MusicCRUD.DataAccess.Entity;

namespace MusicCRUD.DataAccess;

public class MainContext : DbContext 
{

    public DbSet<Music> Music { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer("Data Source=localhost\\SQLDEV;User ID=sa;Password=1;Initial Catalog=MusicCRUD;TrustServerCertificate=True;");
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
