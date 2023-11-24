using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
        public DbSet<StockPurchase> StockPurchases { get; set; }
        public DbSet<StockPurchaseProduct> StockPurchaseProducts { get; set; }
        public DbSet<PayLater> PayLaters { get; set; }

        public string DBPath { get; }

        public Context() 
        {
;           DBPath = System.IO.Path.GetFullPath("sqlite.db");
            Console.WriteLine("DBPath: " + DBPath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DBPath}")
            .LogTo(Console.WriteLine, LogLevel.Information);
        // The following three options help with debugging, but should
        // be changed or removed for production.
        //.LogTo(Console.WriteLine, LogLevel.Information)
        //.EnableSensitiveDataLogging()
        //.EnableDetailedErrors();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPurchase>()
                .HasMany(e => e.Products)
                .WithMany(e => e.StockPurchases)
                .UsingEntity<StockPurchaseProduct>();

            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Transactions)
                .UsingEntity<TransactionProduct>();

            modelBuilder.Entity<Transaction>()
                .HasOne(e => e.PayLater)
                .WithOne(e => e.Transaction)
                .HasForeignKey<PayLater>(e => e.TransactionId)
                .IsRequired();

        User admin = new User { Id = Guid.NewGuid(), Username = "admin", Password = "password" };

            Product product1 = new Product { Id = Guid.NewGuid(), Barcode = "1", CostPrice=11, Name="Product 1", SellingPrice=21, CreatedById=admin.Id, InStock = 2 };
            Product product2 = new Product { Id = Guid.NewGuid(), Barcode = "2", CostPrice=12, Name="Product 2", SellingPrice=22, CreatedById = admin.Id, InStock = 3 };
            Product product3 = new Product { Id = Guid.NewGuid(), Barcode = "3", CostPrice=13, Name="Product 3", SellingPrice=23, CreatedById = admin.Id, InStock = 5 };

            Transaction transaction1 = new Transaction { Id = Guid.NewGuid(), DateTime=DateTime.Now, SellerId = admin.Id };
            TransactionProduct t1A = new TransactionProduct { 
                ProductId = product1.Id, 
                TransactionId = transaction1.Id,
                ProductName = product1.Name,
                CostPrice = product1.CostPrice,
                PriceSold = product1.SellingPrice,
                QuantityBought = 1
            };

            TransactionProduct t1B = new TransactionProduct
            {
                ProductId = product2.Id,
                TransactionId = transaction1.Id,
                ProductName = product2.Name,
                CostPrice = product2.CostPrice,
                PriceSold = product2.SellingPrice,
                QuantityBought = 4
            };


            Transaction transaction2 = new Transaction { Id = Guid.NewGuid(), DateTime=DateTime.Now, SellerId=admin.Id };
            PayLater payLater = new PayLater { Id = Guid.NewGuid(), IsPaid = false, TransactionId = transaction2.Id, PaidAt=DateTime.Now };

            TransactionProduct t2A = new TransactionProduct
            {
                ProductId = product3.Id,
                TransactionId = transaction2.Id,
                ProductName = product3.Name,
                CostPrice = product3.CostPrice,
                PriceSold = product3.SellingPrice,
                QuantityBought = 2
            };

            TransactionProduct t2B = new TransactionProduct
            {
                ProductId = product1.Id,
                TransactionId = transaction2.Id,
                ProductName = product1.Name,
                CostPrice = product1.CostPrice,
                PriceSold = product1.SellingPrice,
                QuantityBought = 3
            };


            StockPurchase stockPurchase = new StockPurchase { Id = Guid.NewGuid(), MadeById = admin.Id, DateTime=DateTime.Now };

            StockPurchaseProduct spp1 = new StockPurchaseProduct { StockPurchaseId = stockPurchase.Id, ProductId = product1.Id, Price = product1.CostPrice, QuantityBought = 2 };
            StockPurchaseProduct spp2 = new StockPurchaseProduct { StockPurchaseId = stockPurchase.Id, ProductId = product2.Id, Price = product2.CostPrice, QuantityBought = 3 };
            StockPurchaseProduct spp3 = new StockPurchaseProduct { StockPurchaseId = stockPurchase.Id, ProductId = product3.Id, Price = product3.CostPrice, QuantityBought = 5 };

            modelBuilder.Entity<User>()
                .HasData(admin);

            modelBuilder.Entity<Product>()
                .HasData(new[] { product1, product2, product3 });

            modelBuilder.Entity<Transaction>()
                .HasData(new[] { transaction1, transaction2 });

            modelBuilder.Entity<TransactionProduct>()
                .HasData(new[] { t1A, t1B, t2A, t2B });

            modelBuilder.Entity<StockPurchase>()
                .HasData(new[] { stockPurchase });

            modelBuilder.Entity<StockPurchaseProduct>()
                .HasData(new[] { spp1, spp2, spp3 });

            modelBuilder.Entity<PayLater>()
                .HasData(new[] { payLater });


        }
    }

}
