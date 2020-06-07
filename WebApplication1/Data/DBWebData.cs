using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data
{
    //1/4/2020 adding IdentityDbContext
    public class DBWebData : IdentityDbContext<Users>
    {
        public DBWebData(DbContextOptions<DBWebData> options) : base(options)
        { 
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                    .Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                    .Property(p => p.UnitPrice)
                    .HasColumnType("decimal(18,2)");
        }
    }
}
