using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class StockPurchaseProductRepository : IStockPurchaseProductRepository
    {

        public StockPurchaseProductRepository(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public void Delete(Guid instanceId)
        {
            StockPurchaseProduct stockPurchaseProduct = _context.StockPurchaseProducts.Find(instanceId);
            if (stockPurchaseProduct != null)
            {
                _context.StockPurchaseProducts.Remove(stockPurchaseProduct);
            }
        }

        public IEnumerable<StockPurchaseProduct> GetAll()
        {
            return _context.StockPurchaseProducts.ToList();
        }

        public StockPurchaseProduct? GetById(Guid instanceId)
        {
            throw new NotImplementedException();
        }

        public StockPurchaseProduct? GetById(Guid stockPurchaseId, Guid productId)
        {
            return _context.StockPurchaseProducts.Find(stockPurchaseId, productId);
        }

        public void Insert(StockPurchaseProduct newInstance)
        {
            _context.StockPurchaseProducts.Add(newInstance);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
