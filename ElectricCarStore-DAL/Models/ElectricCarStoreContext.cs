using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElectricCarStore_DAL.Models;

public partial class ElectricCarStoreContext : DbContext
{
    public ElectricCarStoreContext(DbContextOptions<ElectricCarStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
