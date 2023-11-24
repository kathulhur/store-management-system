using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services.Interfaces
{
    public interface IProductUpdateService
    {

        public ProductUpdateServiceResponse UpdateProduct(Guid productId);
    }

    public enum ProductUpdateServiceResponse
    {
        Success,
        Failed
    }
}
