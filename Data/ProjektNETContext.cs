using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projekt.NET.Models;

namespace Projekt.NET.Data
{
    public class ProjektNETContext : DbContext
    {
        public ProjektNETContext (DbContextOptions<ProjektNETContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SiteUser> User { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<SiteUser>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<CategoryProduct>().HasKey(cp => new { cp.CategoryId, cp.ProductId });
            modelBuilder.Entity<CategoryProduct>()
                .HasOne<Category>(c => c.Category)
                .WithMany(p => p.CategoryProduct)
                .HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<CategoryProduct>()
                .HasOne<Product>(p => p.Product)
                .WithMany(c => c.CategoryProduct)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
