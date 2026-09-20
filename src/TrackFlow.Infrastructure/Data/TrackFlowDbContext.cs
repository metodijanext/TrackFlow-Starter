using Microsoft.EntityFrameworkCore;
using TrackFlow.Domain.Entities;

namespace TrackFlow.Infrastructure.Data;

public class TrackFlowDbContext : DbContext
{
    public TrackFlowDbContext(DbContextOptions<TrackFlowDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<TimeEntry> TimeEntries { get; set; } = null!;
    public DbSet<ProjectUser> ProjectUsers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasMany(e => e.ManagedProjects)
                .WithOne(p => p.Manager)
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.TimeEntries)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.ProjectAssignments)
                .WithOne(pu => pu.User)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Project entity
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);

            entity.HasMany(e => e.TimeEntries)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.TeamMembers)
                .WithOne(pu => pu.Project)
                .HasForeignKey(pu => pu.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure TimeEntry entity
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EntryDate).IsRequired();
            entity.Property(e => e.Hours).HasPrecision(5, 2);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.TaskDescription).HasMaxLength(500);
        });

        // Configure ProjectUser entity (many-to-many junction)
        modelBuilder.Entity<ProjectUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.ProjectId, e.UserId }).IsUnique();
            entity.Property(e => e.AssignedAt).IsRequired();
        });
    }
}
