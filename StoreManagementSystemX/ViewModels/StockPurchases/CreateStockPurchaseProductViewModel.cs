using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.ViewModels.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.StockPurchases
{
    public class CreateStockPurchaseProductViewModel : BaseViewModel, ICreateStockPurchaseProductViewModel
    {
        public CreateStockPurchaseProductViewModel(StockPurchase stockPurchase, Product product, Action<ICreateStockPurchaseProductViewModel> onRemove, Action<ICreateStockPurchaseProductViewModel> onIncrement, Action<ICreateStockPurchaseProductViewModel> onDecrement)
        {
            Product = product;
            StockPurchase = stockPurchase;
            _stockPurchaseProduct = new StockPurchaseProduct
            {
                ProductId = product.Id,
                StockPurchaseId = stockPurchase.Id,
                Price = product.CostPrice,
            };


            _onRemove = onRemove;
            _onIncrement = onIncrement;
            _onDecrement = onDecrement;
            RemoveCommand = new RelayCommand(OnRemove);

            _incrementQuantityCommand = new RelayCommand(OnIncrementQuantity);
            _decrementQuantityCommand = new RelayCommand(OnDecrementQuantity, CanDecrementQuantity);

            // this needs to be executed at the end because it uses the command instances above
            Quantity += 1;
        }

        private readonly Action<ICreateStockPurchaseProductViewModel> _onRemove;
        private readonly Action<ICreateStockPurchaseProductViewModel> _onIncrement;
        private readonly Action<ICreateStockPurchaseProductViewModel> _onDecrement;

        private StockPurchaseProduct _stockPurchaseProduct;

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public Product Product { get; }

        public StockPurchase StockPurchase { get; }

        public string ProductBarcode => Product.Barcode;

        public string ProductName => Product.Name;

        public int Quantity
        {
            get => _stockPurchaseProduct.QuantityBought;
            set
            {
                SetProperty(_stockPurchaseProduct.QuantityBought, value, _stockPurchaseProduct, (u, n) => u.QuantityBought = n);
                Subtotal = Price * Quantity;
                _decrementQuantityCommand.NotifyCanExecuteChanged();
            }
        }

        public decimal Price => Product.CostPrice;

        private decimal _subtotal;
        public decimal Subtotal { get => _subtotal; private set => SetProperty(ref _subtotal, value); }

        public ICommand RemoveCommand { get; }

        private readonly RelayCommand _incrementQuantityCommand;

        public ICommand IncrementQuantityCommand => _incrementQuantityCommand;

        private readonly RelayCommand _decrementQuantityCommand;
        public ICommand DecrementQuantityCommand => _decrementQuantityCommand;

        private void OnRemove()
        {
            _onRemove(this);
        }

        private void OnIncrementQuantity()
        {
            Quantity += 1;
            _onIncrement(this);
        }

        private void OnDecrementQuantity()
        {
            Quantity -= 1;
            _onDecrement(this);
        }

        private bool CanDecrementQuantity() => Quantity > 1;

        private void Reset()
        {
            _stockPurchaseProduct = new StockPurchaseProduct
            {
                ProductId = Product.Id,
                StockPurchaseId = StockPurchase.Id,
                Price = Product.CostPrice,
            };
            Quantity = 1;
        }

        public StockPurchaseProduct BuildStockPurchaseProduct()
        {
            var newStockPurchaseProduct = _stockPurchaseProduct;
            Product.InStock += Quantity;
            Reset();
            return newStockPurchaseProduct;
            
        }
    }
}
