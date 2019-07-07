using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMMC.Auth.DataAccess.Models;
using VMMC.Auth.Web.API.Db;

namespace VMMC.Auth.Web.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Funders> Funders { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<ServiceProviders> ServiceProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Tokens).WithOne(t => t.User);

            modelBuilder.Entity<Token>().ToTable("Tokens");
            modelBuilder.Entity<Token>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Token>().HasOne(t => t.User).WithMany(u => u.Tokens);

            modelBuilder.Entity<Funders>(entity =>
            {
                entity.HasKey(e => e.FunderId);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModified).HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnType("bit");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Partners>(entity =>
            {
                entity.HasKey(e => e.PartnerId);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModified).HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.IsDeleted).HasColumnType("bit");

                entity.HasOne(d => d.Funder)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.FunderId)
                    .HasConstraintName("FK_Partners_Funders");
            });

            modelBuilder.Entity<ServiceProviders>(entity =>
            {
                entity.HasKey(e => e.ProviderId);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModified).HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.IsDeleted).HasColumnType("bool");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.ServiceProviders)
                    .HasForeignKey(d => d.PartnerId)
                    .HasConstraintName("FK_ServiceDeliveries_Partners");
            });
        }


    }
}
