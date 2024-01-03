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
    public class ProductCreationService : IProductCreationService
    {
        
        public ProductCreationService(Domain.Repositories.Products.Interfaces.IProductRepository productRepository, IBarcodeImageService barcodeImageService)
        {
            _productRepository = productRepository;
            _barcodeImageService = barcodeImageService;
        }

        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;
        private readonly IBarcodeImageService _barcodeImageService;
        public Guid? CreateNewProduct(AuthContext authContext)
        {
            Guid? newProductId = null;
            var window = new CreateProductWindow(authContext, _productRepository, _barcodeImageService, (Guid Id) =>
            {
                newProductId = Id;
            });
            window.ShowDialog();

            return newProductId;
            
        }
    }
}
