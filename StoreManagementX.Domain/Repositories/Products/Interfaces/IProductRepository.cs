using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories.Products.Interfaces
{
    public interface IProductRepository : IRepository<IProduct>
    {
        public IProduct? GetByBarcode(string barcode);
    }
}
