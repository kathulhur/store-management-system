using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.Views.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services
{
    public class ProductUpdateService : IProductUpdateService
    {
        public ProductUpdateService(Domain.Repositories.Products.Interfaces.IProductRepository productRepository)
        {
            _productRepoisitory = productRepository;
        }

        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepoisitory;

        public ProductUpdateServiceResponse UpdateProduct(Guid productId)
        {
            ProductUpdateServiceResponse response = ProductUpdateServiceResponse.Failed;

            var window = new UpdateProductWindow(productId, _productRepoisitory, (ProductUpdateServiceResponse status) =>
            {
                response = status;
            });
            window.ShowDialog();    


            return response;
        }
    }
}
