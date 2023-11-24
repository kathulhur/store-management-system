using SQLitePCL;
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
    public class ProductUpdateService : IProductUpdateService
    {
        public ProductUpdateService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProductUpdateServiceResponse UpdateProduct(Guid productId)
        {
            ProductUpdateServiceResponse response = ProductUpdateServiceResponse.Failed;

            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var window = new UpdateProductWindow(productId, unitOfWork, (ProductUpdateServiceResponse status) =>
                {
                    response = status;
                });
                window.ShowDialog();    
            }


            return response;
        }
    }
}
