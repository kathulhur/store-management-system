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
    public class ProductRepository : IProductRepository, IDisposable
    {

        public ProductRepository(Context context)
        {
            _context = context;
        }

        private readonly Context _context;


        public Product? GetByBarcode(string barcode)
            => _context.Products.Where(p => p.Barcode == barcode).FirstOrDefault();

        public IEnumerable<Product> GetAll()
            => _context.Products.ToList();

        public Product? GetById(Guid instanceId)
            => _context.Products.Find(instanceId);

        public void Insert(Product newInstance)
        {
            _context.Products.Add(newInstance);
        }


        public void Delete(Guid instanceId)
        {
            Product product = _context.Products.Find(instanceId);
            _context.Products.Remove(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Attach(Product product)
        {
            _context.Attach(product);
        }

        public void Detach(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Detached;
        }

    }
}
