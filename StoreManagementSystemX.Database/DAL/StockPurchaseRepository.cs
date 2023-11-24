using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class StockPurchaseRepository : IStockPurchaseRepository
    {

        public StockPurchaseRepository(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public void Delete(Guid stockPurchaseId)
        {
            StockPurchase purchase = _context.StockPurchases.Find(stockPurchaseId);
            _context.Remove(purchase);
        }

        public IEnumerable<StockPurchase> GetAll()
        {
            return _context.StockPurchases.Include(sp => sp.StockPurchaseProducts).ThenInclude(e => e.Product).ToList();
        }

        public StockPurchase? GetById(Guid stockPurchaseId)
        {
            return _context.StockPurchases
                .Include(e => e.StockPurchaseProducts)
                .ThenInclude(e => e.Product)
                .Where(e => e.Id == stockPurchaseId).FirstOrDefault();
        }

        public void Insert(StockPurchase stockPurchase)
        {
            _context.StockPurchases.Add(stockPurchase);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
