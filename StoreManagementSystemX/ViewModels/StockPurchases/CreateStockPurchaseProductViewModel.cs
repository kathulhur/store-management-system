using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
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
        internal CreateStockPurchaseProductViewModel(IStockPurchase stockPurchase, IProduct product, Action<CreateStockPurchaseProductViewModel> onRemove, Action<CreateStockPurchaseProductViewModel> onIncrement, Action<CreateStockPurchaseProductViewModel> onDecrement)
        {
            _stockPurchase = stockPurchase;
            _product = product;

            _incrementQuantityCommand = new RelayCommand(OnIncrementQuantity);
            _decrementQuantityCommand = new RelayCommand(OnDecrementQuantity, CanDecrementQuantity);

            Barcode = product.Barcode;
            Name = product.Name;
            Price = product.CostPrice;
            _onRemove = onRemove;
            _onIncrement = onIncrement;
            _onDecrement = onDecrement;
            RemoveCommand = new RelayCommand(OnRemove);

            // this needs to be executed at the end because it uses the command instances above
        }

        private readonly Action<CreateStockPurchaseProductViewModel> _onRemove;
        private readonly Action<CreateStockPurchaseProductViewModel> _onIncrement;
        private readonly Action<CreateStockPurchaseProductViewModel> _onDecrement;

        private IStockPurchase _stockPurchase;

        private IProduct _product;


        public string Barcode { get; }

        public string Name { get; }

        public int Quantity => _stockPurchase.StockPurchaseProducts.First(e => e.Barcode == _product.Barcode).QuantityBought;

        public decimal Price { get; }

        public decimal TotalPrice => _stockPurchase.StockPurchaseProducts.First(e => e.Barcode == _product.Barcode).TotalCost;


        public ICommand RemoveCommand { get; }

        private readonly RelayCommand _incrementQuantityCommand;

        public ICommand IncrementQuantityCommand => _incrementQuantityCommand;

        private readonly RelayCommand _decrementQuantityCommand;
        public ICommand DecrementQuantityCommand => _decrementQuantityCommand;

        private void OnRemove()
        {
            _stockPurchase.RemoveProduct(_product);
            _onRemove(this);
        }

        private void OnIncrementQuantity()
        {
            _stockPurchase.IncrementProduct(_product);
            _onIncrement(this);
            OnPropertyChanged(nameof(Quantity));
            _decrementQuantityCommand.NotifyCanExecuteChanged();
        }

        private void OnDecrementQuantity()
        {
            _stockPurchase.DecrementProduct(_product);
            OnPropertyChanged(nameof(Quantity));
            _onDecrement(this);
            _decrementQuantityCommand.NotifyCanExecuteChanged();
        }


        private bool CanDecrementQuantity() => Quantity > 1;

    }
}
