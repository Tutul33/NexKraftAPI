﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ORM.DataModels;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TUTULPC\\MSSQL_SRV_TUTUL;Database=NexKraftDB;User Id=sa; Password=@sa12345#;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Country).HasMaxLength(150);
            entity.Property(e => e.CustomerName).HasMaxLength(300);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
