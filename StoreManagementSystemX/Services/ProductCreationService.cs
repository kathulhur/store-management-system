using StoreManagementSystemX.Database.DAL.Interfaces;
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
        
        public ProductCreationService(IUnitOfWorkFactory unitOfWorkFactory, IBarcodeGeneratorService barcodeGeneratorService, IBarcodeImageService barcodeImageService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _barcodeGeneratorService = barcodeGeneratorService;
            _barcodeImageService = barcodeImageService;
        }

        private readonly IBarcodeImageService _barcodeImageService;
        private readonly IBarcodeGeneratorService _barcodeGeneratorService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public Guid? CreateNewProduct(AuthContext authContext)
        {
            using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                Guid? newProductId = null;
                var window = new CreateProductWindow(authContext, unitOfWork, _barcodeGeneratorService, _barcodeImageService, (Guid Id) =>
                {
                    newProductId = Id;
                });
                window.ShowDialog();

                return newProductId;
            }
        }
    }
}
