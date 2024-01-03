using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Products.Interfaces
{
    public interface IProductFactory
    {
        public IProduct Create(ICreateProductArgs createProductArgs);

        public IProduct Reconstitute(IProductReconstitutionArgs productReconstitutionArgs);
    }
}
