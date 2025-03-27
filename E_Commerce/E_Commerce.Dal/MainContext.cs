using E_Commerce.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Dal;

public class MainContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Card> Cards { get; set; }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Cart>().ToTable("Cart");
        modelBuilder.Entity<Card>().ToTable("Card");
        modelBuilder.Entity<CartProduct>().ToTable("CartProduct");
        modelBuilder.Entity<OrderProduct>().ToTable("OrderProduct");
        modelBuilder.Entity<Payment>().ToTable("Payment");
        modelBuilder.Entity<OrderStatus>().ToTable("OrderStatus");
    }
}
