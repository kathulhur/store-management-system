using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.ViewModels.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Products
{
    class CreateProductViewModel : BaseViewModel, ICreateProductViewModel
    {
        public CreateProductViewModel(AuthContext authContext, IUnitOfWork unitOfWork, Action<Guid> onAdd, Action closeWindow)
        {
            _authContext = authContext;
            _unitOfWork = unitOfWork;
            _onAdd = onAdd;
            _closeWindow = closeWindow;
            SubmitCommand = new RelayCommand(SubmitCommandHandler);
            CancelCommand = new RelayCommand(CancelCommandHandler);
        }

        private string _barcode = string.Empty;
        public string Barcode { get => _barcode; set => SetProperty(ref _barcode, value); }

        private string _name = string.Empty;
        public string Name { get => _name; set => SetProperty(ref _name, value); }


        private decimal _costPrice;
        public decimal CostPrice { get => _costPrice; set => SetProperty(ref _costPrice, value); }

        private decimal _sellingPrice;
        public decimal SellingPrice { get => _sellingPrice; set => SetProperty(ref _sellingPrice, value); }


        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

        private void CancelCommandHandler()
        {
            _closeWindow();
        }

        private void SubmitCommandHandler()
        {
            var newProduct = BuildProduct();
            _unitOfWork.ProductRepository.Insert(newProduct);
            _unitOfWork.Save();
            _onAdd(newProduct.Id);
            _closeWindow();
        }

        private Product BuildProduct()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Barcode = Barcode,
                CostPrice = CostPrice,
                SellingPrice = SellingPrice,
                CreatedById = _authContext.CurrentUser.Id
            };
        }

        private readonly AuthContext _authContext;

        private readonly Action _closeWindow;

        private readonly IUnitOfWork _unitOfWork;

        private readonly Action<Guid> _onAdd;


    }
}
