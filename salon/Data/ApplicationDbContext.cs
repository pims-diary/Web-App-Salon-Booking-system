using Microsoft.EntityFrameworkCore;
using Salon.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Salon.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed two sample staff records
            modelBuilder.Entity<Staff>().HasData(
                new Staff { Id = 1, StaffId = "staff1", Password = "password1" },
                new Staff { Id = 2, StaffId = "staff2", Password = "password2" }
            );

            // Seed two sample promotions
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion { Id = 1, Title = "Black Friday Sale", Description = "50% off all haircuts – one day only!" },
                new Promotion { Id = 2, Title = "Combo Offer", Description = "Haircut + Facial in a combo price for the weekend." },
                new Promotion { Id = 3, Title = "New Customer Special", Description = "20% off on the first visit of any new customer." }
            );

            
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Classic Haircut", Category = "Service", Price = 30m, Description = "Basic haircut with style.", CreatedDate = DateTime.Today.AddDays(-10) },
                new Product { Id = 2, Name = "Hair Spa Treatment", Category = "Service", Price = 60m, Description = "Deep nourishment for hair.", CreatedDate = DateTime.Today.AddDays(-5) },
                new Product { Id = 3, Name = "Argan Oil Shampoo", Category = "Retail", Price = 25m, Description = "Premium shampoo for smooth hair.", CreatedDate = DateTime.Today.AddDays(-2) }
);
        }
    }
}
