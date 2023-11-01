namespace MusicHub.Data;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public class MusicHubDbContext : DbContext
{
    public MusicHubDbContext()
    {
    }

    public MusicHubDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Configuration.ConnectionString);
        }
    }

    public DbSet<Album> Albums { get; set; } = null!;

    public DbSet<Performer> Performers { get; set; } = null!;

    public DbSet<Producer> Producers { get; set; } = null!;

    public DbSet<Song> Songs { get; set; } = null!;

    public DbSet<SongPerformer> SongsPerformers { get; set; } = null!;

    public DbSet<Writer> Writers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Album>(entity =>
        {
            entity
                .Property(a => a.ReleaseDate)
                .HasColumnType("date");
        });

        builder.Entity<Song>(entity =>
        {
            entity
                .Property(s => s.CreatedOn)
                .HasColumnType("date");
        });
    }
}