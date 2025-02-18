using CarRentalSystem.Dal.Entities;
using CarRentalSystem.Dal.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Dal;

public class MainContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }


    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new CartProductConfiguration());
    }
}

