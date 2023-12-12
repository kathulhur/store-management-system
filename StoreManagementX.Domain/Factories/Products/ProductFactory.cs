using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;

namespace StoreManagementSystemX.Domain.Factories.Products
{
    public class ProductFactory : IProductFactory
    {

        public IProduct Create(ICreateProductArgs args)
        {
            return new Product(args.CreatorId, Guid.NewGuid(), args.Name, args.CostPrice, args.SellingPrice, args.InStock);
        }
    }
}
