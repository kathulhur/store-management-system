using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Interfaces;
using StoreManagementSystemX.ViewModels.Products.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace StoreManagementSystemX.ViewModels.Products
{
    public class InventoryViewModel : BaseViewModel, IInventoryViewModel
    {
        public InventoryViewModel(AuthContext authContext, IProductRepository productRepository, IDialogService dialogService, IProductUpdateService productUpdateService, IProductCreationService productCreationService, IBarcodeImageService barcodeImageService)
        {
            _authContext = authContext;
            _dialogService = dialogService;
            _productUpdateService = productUpdateService;
            _productCreationService = productCreationService;
            _barcodeImageService = barcodeImageService;
            _productRepository = productRepository;

            Products = new ObservableCollection<IProductRow>();
            
            foreach (IProduct p in productRepository.GetAll())
            {
                AddProductToList(p);
            }

            AddProductCommand = new RelayCommand(AddProductCommandHandler);
        }

        private readonly IBarcodeImageService _barcodeImageService;
        private readonly IProductCreationService _productCreationService;

        private readonly IProductUpdateService _productUpdateService;

        private readonly IProductRepository _productRepository;

        private AuthContext _authContext { get; }

        private readonly IDialogService _dialogService;

        public ObservableCollection<IProductRow> Products { get; }

        public ICommand AddProductCommand { get; }


        private void AddProductCommandHandler()
        {
            // TODO: Create a unit of work factory
            var newProductId = _productCreationService.CreateNewProduct(_authContext);
            if (newProductId != null && newProductId.HasValue)
            {
                
                var newProduct = _productRepository.GetById((Guid)newProductId);
                if (newProduct != null)
                {
                    AddNewlyCreatedProduct(newProduct);
                }
            }
        }

        private void AddNewlyCreatedProduct(IProduct newProduct)
        {
            AddProductToList(newProduct);
        }

        private void AddProductToList(IProduct product)
        {

            var productRow = new ProductRow(this, product);
            SubscribeToProductRowEvents(productRow);
            Products.Add(productRow);
        }

        private void SubscribeToProductRowEvents(IProductRow productRow)
        {
            productRow.ProductDeleted += HandleProductDeletion;
        }

        private void UnsubscribeToProductRowEvents(IProductRow productRow)
        {
            productRow.ProductDeleted -= HandleProductDeletion;
        }


        private void HandleProductDeletion(object? sender, EventArgs<IProductRow> e)
        {
            UnsubscribeToProductRowEvents(e.Item);
            Products.Remove(e.Item);
        }


        class ProductRow : ObservableObject, IProductRow
        {
            InventoryViewModel _parent;

            public ProductRow(InventoryViewModel parent, IProduct product)
            {
                _parent = parent;
                _product = product;
                UpdateCommand = new RelayCommand(UpdateCommandHandler);
                DeleteCommand = new RelayCommand(DeleteCommandHandler);
                BarcodeImage = _parent._barcodeImageService.GenerateBarcodeImage(product.Barcode);
            }

            private IProduct _product;

            public Guid Id => _product.Id;

            public ImageSource BarcodeImage { get; }

            public string Barcode => _product.Barcode;

            public string Name { get => _product.Name; private set => SetProperty(_product.Name, value, _product, (u, n) => u.Name = n); }

            public int InStock => _product.InStock;

            public decimal CostPrice { get => _product.CostPrice; private set => SetProperty(_product.CostPrice, value, _product, (u, n) => u.CostPrice = n); }

            public decimal SellingPrice { get => _product.SellingPrice; private set => SetProperty(_product.SellingPrice, value, _product, (u, n) => u.SellingPrice = n); }

            public ICommand UpdateCommand { get; }
            private void UpdateCommandHandler()
            {
                var updateStatus = _parent._productUpdateService.UpdateProduct(_product.Id);
                if (updateStatus == ProductUpdateServiceResponse.Success)
                {
                    var updatedProduct = _parent._productRepository.GetById(_product.Id);
                    if(_product != null)
                    {
                        _product = updatedProduct;
                        NotifyPropertiesChanged();
                    }
                }
            }

            private void NotifyPropertiesChanged()
            {
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CostPrice));
                OnPropertyChanged(nameof(SellingPrice));
            }


            private void OnProductDeleted(EventArgs<IProductRow> e)
            {
                ProductDeleted?.Invoke(this, e);
            }

            public event EventHandler<EventArgs<IProductRow>> ProductDeleted = null!;
            public ICommand DeleteCommand { get; }
            private void DeleteCommandHandler()
            {
                if (_parent._dialogService.ShowConfirmationDialog("Confirm Delete", "Are you sure you want to delete this product?"))
                {
                        _parent._productRepository.Remove(_product.Id);
                    OnProductDeleted(new EventArgs<IProductRow>(this));
                }

            }

        }

    }
}
