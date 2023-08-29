using AspNetJWT.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetJWT.Infrastructure;

public class ItemsContext : DbContext
{
    public ItemsContext(DbContextOptions<ItemsContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>().HasData(
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "TV",
                Stock = 100,
                Price = 15000
            }
        );

        modelBuilder.Entity<Item>().HasData(
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Tablet",
                Stock = 10,
                Price = 25000
            }
        );
    }

    public DbSet<Item> Items { get; set; }
}