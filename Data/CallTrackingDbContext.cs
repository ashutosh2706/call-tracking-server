using System;
using System.Collections.Generic;
using CallServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CallServer.Data;

public partial class CallTrackingDbContext : DbContext
{
    public CallTrackingDbContext() {}

    public CallTrackingDbContext(DbContextOptions<CallTrackingDbContext> options) : base(options) {}

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<CallDetail> CallDetails { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("PK__Agent__9AC3BFF10B8CC2D3");

            entity.ToTable("Agent");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Status).WithMany(p => p.Agents)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Agent__StatusId__3C69FB99");

            entity.HasMany(d => d.Hids).WithMany(p => p.Agents)
                .UsingEntity<Dictionary<string, object>>(
                    "AgentHospital",
                    r => r.HasOne<Hospital>().WithMany()
                        .HasForeignKey("Hid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AgentHospit__HId__45F365D3"),
                    l => l.HasOne<Agent>().WithMany()
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AgentHosp__Agent__44FF419A"),
                    j =>
                    {
                        j.HasKey("AgentId", "Hid").HasName("PK__AgentHos__F6B6EEA5C8F704E7");
                        j.ToTable("AgentHospital");
                        j.IndexerProperty<long>("Hid").HasColumnName("HId");
                    });
        });

        modelBuilder.Entity<CallDetail>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PK__CallDeta__5180CFAA007331B6");

            entity.ToTable("CallDetail");

            entity.Property(e => e.CallId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Hid).HasColumnName("HId");

            entity.HasOne(d => d.Agent).WithMany(p => p.CallDetails)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK__CallDetai__Agent__5441852A");

            entity.HasOne(d => d.HidNavigation).WithMany(p => p.CallDetails)
                .HasForeignKey(d => d.Hid)
                .HasConstraintName("FK__CallDetail__HId__534D60F1");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.Hid).HasName("PK__Hospital__C755154734B9BABD");

            entity.ToTable("Hospital");

            entity.Property(e => e.Hid).HasColumnName("HId");
            entity.Property(e => e.Hname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HName");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2063D694A987");

            entity.ToTable("Status");

            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
