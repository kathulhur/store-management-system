using Accessibility;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Products
{
    public class UpdateProductViewModel : ObservableObject, IUpdateProductViewModel
    {
        public UpdateProductViewModel(Guid productId, Domain.Repositories.Products.Interfaces.IProductRepository productRepository, Action closeWindow, Action<ProductUpdateServiceResponse> onAction)
        {
            CancelCommand = new RelayCommand(OnCancel);
            SubmitCommand = new RelayCommand(UpdateProduct);
            _closeWindow = closeWindow;
            _productRepository = productRepository;
            _onAction = onAction;
            // TODO: show message box and close if product with `productId` doesn't exist
            _product = _productRepository.GetById(productId);

        }

        private readonly Action _closeWindow;
        private readonly Action<ProductUpdateServiceResponse> _onAction;
        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;
        private readonly IProduct _product;

        public string Name { get => _product.Name; set => SetProperty(_product.Name, value, _product, (u, n) => u.Name = n); }

        public decimal CostPrice { get => _product.CostPrice; set => SetProperty(_product.CostPrice, value, _product, (u, n) => u.CostPrice = n); }

        public decimal SellingPrice { get => _product.SellingPrice; set => SetProperty(_product.SellingPrice, value, _product, (u, n) => u.SellingPrice = n); }

        public ICommand CancelCommand { get; }

        public ICommand SubmitCommand { get; }

        private void OnCancel()
        {

            _onAction(ProductUpdateServiceResponse.Failed);
            _closeWindow();
        }

        public void UpdateProduct()
        {
            _productRepository.Update(_product);
            _onAction(ProductUpdateServiceResponse.Success);
            _closeWindow();
        }
    }
}
