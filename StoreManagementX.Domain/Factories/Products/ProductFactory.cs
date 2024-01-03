using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Services.Barcode.Interfaces;

namespace StoreManagementSystemX.Domain.Factories.Products
{
    public class ProductFactory : IProductFactory
    {
        public ProductFactory(IBarcodeGenerationService barcodeGenerationService)
        {
            _barcodeGenerationService = barcodeGenerationService;
        }

        private readonly IBarcodeGenerationService _barcodeGenerationService;

        public IProduct Create(ICreateProductArgs args)
        {
            if (args.Barcode == null)
            {
                return new Product(args.CreatorId, Guid.NewGuid(), _barcodeGenerationService.GenerateBarcode(), args.Name, args.CostPrice, args.SellingPrice);
            } else
            {
                return new Product(args.CreatorId, Guid.NewGuid(), args.Barcode, args.Name, args.CostPrice, args.SellingPrice);
            }
        }

        public IProduct Reconstitute(IProductReconstitutionArgs productReconstitutionArgs)
        {
            return new Product(
                productReconstitutionArgs.CreatorId,
                productReconstitutionArgs.Id,
                productReconstitutionArgs.Barcode,
                productReconstitutionArgs.Name,
                productReconstitutionArgs.CostPrice,
                productReconstitutionArgs.SellingPrice,
                productReconstitutionArgs.InStock
            );
        }
    }
}
