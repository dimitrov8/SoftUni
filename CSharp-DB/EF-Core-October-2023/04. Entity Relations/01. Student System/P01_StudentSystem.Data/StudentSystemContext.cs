namespace P01_StudentSystem.Data;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public class StudentSystemContext : DbContext
{
    public StudentSystemContext()
    {
    }

    public StudentSystemContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;

    public DbSet<Homework> Homeworks { get; set; } = null!;

    public DbSet<Resource> Resources { get; set; } = null!;

    public DbSet<StudentCourse> StudentsCourses { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(DbConfig.CONNECTION_STRING);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("char(10)");
        });

        modelBuilder.Entity<StudentCourse>(entity => { entity.HasKey(pk => new { pk.StudentId, pk.CourseId }); });
    }
}