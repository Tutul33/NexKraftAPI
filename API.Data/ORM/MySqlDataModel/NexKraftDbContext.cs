using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ORM.MySqlDataModel;

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

    public virtual DbSet<Customerlogin> Customerlogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=127.0.0.1;Database=NexKraftDB;User Id=root; Password=@Root12345#;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Country).HasMaxLength(150);
            entity.Property(e => e.CustomerName).HasMaxLength(300);
        });

        modelBuilder.Entity<Customerlogin>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PRIMARY");

            entity.ToTable("customerlogin");

            entity.HasIndex(e => e.CustomerId, "CustomerID_idx");

            entity.Property(e => e.LoginId).HasColumnName("loginId");
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.Customerlogins)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("CustomerID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
