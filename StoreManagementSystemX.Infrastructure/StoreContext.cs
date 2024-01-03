using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreManagementSystemX.Infrastructure.DTO;
using StoreManagementSystemX.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Transaction;

namespace StoreManagementSystemX.Database
{
    public class StoreContext : DbContext
    {
        public DbSet<UserDBModel> Users { get; set; }
        public DbSet<ProductDBModel> Products { get; set; }
        public DbSet<TransactionDBModel> Transactions { get; set; }

        public string DBPath { get; }

        public StoreContext() 
        {
;           DBPath = System.IO.Path.GetFullPath("sqlite.db");
            Console.WriteLine("DBPath: " + DBPath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DBPath}")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(Console.WriteLine, LogLevel.Information);
        // The following three options help with debugging, but should
        // be changed or removed for production.
        //.LogTo(Console.WriteLine, LogLevel.Information)
        //.EnableSensitiveDataLogging()
        //.EnableDetailedErrors();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionDBModel>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Transactions)
                .UsingEntity<TransactionProductDBModel>();

            modelBuilder.Entity<StockPurchaseDBModel>()
                .HasMany(e => e.Products)
                .WithMany(e => e.StockPurchases)
                .UsingEntity<StockPurchaseProductDBModel>();

            UserDBModel admin = new UserDBModel { Id = Guid.NewGuid(), Username = "admin", Password = "password" };
            modelBuilder.Entity<UserDBModel>()
                .HasData(admin);


        }
    }

}
