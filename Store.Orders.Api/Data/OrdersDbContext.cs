using Microsoft.EntityFrameworkCore;
using Store.Orders.Api.Domain;

namespace Store.Orders.Api.Data;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(o => o.AppliedPrice).HasPrecision(18, 2);
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Ноутбук", Price = 35000, Description = "Потужний девайс" },
            new Product { Id = 2, Name = "Мишка", Price = 1200, Description = "Ігрова" }
        );
    }
}