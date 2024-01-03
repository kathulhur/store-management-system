using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.Persistence
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly IProductFactory _productFactory;
        private DbContext _dbContext;
        private DbSet<ProductDBModel> _productsDTOs;

        public ProductRepositoryImpl(IProductFactory productFactory)
        {
            _productFactory = productFactory;
            _dbContext = new StoreContext();
            _productsDTOs = _dbContext.Set<ProductDBModel>();

        }
        public void Add(IProduct newProduct)
        {
            var productToAdd = new ProductDTO(newProduct).ToDBModel();
            _productsDTOs.Add(productToAdd);
            _dbContext.SaveChanges();
        }

        public IEnumerable<IProduct> GetAll()
        {
            var storedProductDTOs = _productsDTOs.ToList();
            var products = new List<IProduct>();
            foreach (var productDTO in storedProductDTOs)
            {
                products.Add(_productFactory.Reconstitute(productDTO));
            }

            return products;
        }

        public IProduct? GetByBarcode(string barcode)
        {
            var matchedProduct = _productsDTOs.SingleOrDefault(p => p.Barcode == barcode);
            if (matchedProduct != null)
            {
                return _productFactory.Reconstitute(matchedProduct);
            }

            return null;
        }

        public IProduct? GetById(Guid id)
        {
            var matchedProduct = _productsDTOs.Find(id);
            if (matchedProduct != null)
            {
                return _productFactory.Reconstitute(matchedProduct);
            }

            return null;
        }

        public void Remove(Guid id)
        {
            var productToDelete = _productsDTOs.Find(id);
            if (productToDelete != null)
            {
                _productsDTOs.Remove(productToDelete);
            }
            _dbContext.SaveChanges();
        }

        public void Update(IProduct newEntity)
        {
            var productToUpdate = new ProductDTO(newEntity).ToDBModel();
            var productEntry = _productsDTOs.Entry(productToUpdate);
            productEntry.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
