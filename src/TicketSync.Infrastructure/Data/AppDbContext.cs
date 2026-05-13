using Microsoft.EntityFrameworkCore;
using TicketSync.Core.Models;

namespace TicketSync.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TicketMapping> TicketMappings { get; set; } = null!;
    public DbSet<SyncLog> SyncLogs { get; set; } = null!;
    public DbSet<TicketFieldSnapshot> TicketFieldSnapshots { get; set; } = null!;
    public DbSet<SyncRetry> SyncRetries { get; set; } = null!;
    public DbSet<FieldMappingConfig> FieldMappingConfigs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TicketMapping configuration
        modelBuilder.Entity<TicketMapping>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.JiraTicketKey).IsRequired().HasMaxLength(50);
            entity.Property(e => e.JiraTicketId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.AseeTicketId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SyncStatus).IsRequired().HasMaxLength(20).HasDefaultValue("ACTIVE");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(e => e.JiraTicketKey).IsUnique();
            entity.HasIndex(e => e.AseeTicketId).IsUnique();
            entity.HasIndex(e => e.SyncStatus);

            entity.HasMany(e => e.SyncLogs)
                .WithOne(s => s.TicketMapping)
                .HasForeignKey(s => s.TicketMappingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.FieldSnapshots)
                .WithOne(s => s.TicketMapping)
                .HasForeignKey(s => s.TicketMappingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.SyncRetries)
                .WithOne(s => s.TicketMapping)
                .HasForeignKey(s => s.TicketMappingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // SyncLog configuration
        modelBuilder.Entity<SyncLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SyncDirection).IsRequired().HasMaxLength(20);
            entity.Property(e => e.ActionType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.SourceSystem).IsRequired().HasMaxLength(20);
            entity.Property(e => e.TargetSystem).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("SUCCESS");
            entity.Property(e => e.ExecutedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(e => e.TicketMappingId);
            entity.HasIndex(e => e.SyncDirection);
            entity.HasIndex(e => e.ExecutedAt);
        });

        // TicketFieldSnapshot configuration
        modelBuilder.Entity<TicketFieldSnapshot>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SnapshotSystem).IsRequired().HasMaxLength(20);
            entity.Property(e => e.FieldName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ChangedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(e => e.TicketMappingId);
            entity.HasIndex(e => e.ChangedAt);
        });

        // SyncRetry configuration
        modelBuilder.Entity<SyncRetry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SyncDirection).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("PENDING");

            entity.HasIndex(e => e.TicketMappingId);
            entity.HasIndex(e => e.NextRetryAt);
        });

        // FieldMappingConfig configuration
        modelBuilder.Entity<FieldMappingConfig>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.JiraFieldName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.AseeFieldName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FieldType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(e => new { e.JiraFieldName, e.AseeFieldName }).IsUnique();
        });
    }
}
