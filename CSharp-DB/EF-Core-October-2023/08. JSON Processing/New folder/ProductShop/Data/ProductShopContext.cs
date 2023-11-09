﻿namespace ProductShop.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class ProductShopContext : DbContext
{
    public ProductShopContext()
    {
    }

    public ProductShopContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<CategoryProduct> CategoriesProducts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Configuration.CONNECTION_STRING)
                .UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryProduct>(entity => { entity.HasKey(x => new { x.CategoryId, x.ProductId }); });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(x => x.ProductsBought)
                .WithOne(x => x.Buyer)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(x => x.ProductsSold)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}