﻿using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories
{
    public class ProductRepository : IRepository<IProduct>
    {
        private readonly List<IProduct> _products;

        public ProductRepository()
        {
            _products = new List<IProduct>();
        }


        public void Add(IProduct newEntity)
        {
            _products.Add(newEntity);
        }

        public IEnumerable<IProduct> GetAll()
        {
            return _products;
        }

        public IProduct? GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Remove(Guid id)
        {
            var productToRemove = _products.FirstOrDefault(_ => _.Id == id);

            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
        }

        public void Update(IProduct updatedProduct)
        {
            var productIndex = _products.FindIndex(p => p.Id == updatedProduct.Id);

            if (productIndex != -1)
            {
                _products[productIndex] = updatedProduct;
            }
        }
    }
}
