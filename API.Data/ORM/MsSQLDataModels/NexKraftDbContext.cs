using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ORM.MsSQLDataModels;

public partial class NexKraftDbContext : DbContext
{
    public NexKraftDbContext()
    {
    }

    public NexKraftDbContext(DbContextOptions<NexKraftDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-LPDBEG4\\SQL_SRV_NEXKRAFT;Database=NexKraftDB;User Id=sa; Password=@sa12345#;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Country).HasMaxLength(150);
            entity.Property(e => e.CustomerName).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(50);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.LoginId);

            entity.ToTable("UserLogin");

            entity.Property(e => e.LoginId).HasColumnName("LoginID");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_UserLogin_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
