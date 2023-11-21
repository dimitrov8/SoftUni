namespace Invoices.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class InvoicesContext : DbContext
{
    public InvoicesContext()
    {
    }

    public InvoicesContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Configuration.ConnectionString)
                .UseLazyLoadingProxies();
        }
    }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<ProductClient> ProductsClients { get; set; } = null!;

    public DbSet<Client> Clients { get; set; }

    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<Invoice> Invoices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.IssueDate)
                .HasColumnType("date");

            entity.Property(e => e.DueDate)
                .HasColumnType("date");

            entity.HasOne(e => e.Client)
                .WithMany(e => e.Invoices);
        });

        modelBuilder.Entity<ProductClient>(entity =>
        {
            entity.HasKey(pc => new { pc.ProductId, pc.ClientId });

            entity.HasOne(e => e.Product)
                .WithMany(e => e.ProductsClients);

            entity.HasOne(e => e.Client)
                .WithMany(e => e.ProductsClients);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasOne(e => e.Client)
                .WithMany(e => e.Addresses);
        });
    }
}