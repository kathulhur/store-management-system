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
        
        public ProductCreationService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public Guid? CreateNewProduct(AuthContext authContext)
        {
            using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                Guid? newProductId = null;
                var window = new CreateProductWindow(authContext, unitOfWork, (Guid Id) =>
                {
                    newProductId = Id;
                });
                window.ShowDialog();

                return newProductId;
            }
        }
    }
}
