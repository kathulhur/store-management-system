using CommunityToolkit.Mvvm.ComponentModel;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.ViewModels.StockPurchases
{
    public class StockPurchaseProductViewModel : ObservableObject
    {
        public StockPurchaseProductViewModel(StockPurchaseProduct stockPurchaseProduct)
        {
            _stockPurchaseProduct = stockPurchaseProduct;
        }

        private readonly StockPurchaseProduct _stockPurchaseProduct;

        public Guid ProductId => _stockPurchaseProduct.ProductId;

        public Guid TransactionId => _stockPurchaseProduct.StockPurchaseId;

        public string Name => _stockPurchaseProduct.Product.Name;

        public string Barcode => _stockPurchaseProduct.Product.Barcode;

        public decimal Price => _stockPurchaseProduct.Price;

        public int Quantity => _stockPurchaseProduct.QuantityBought;

        public decimal TotalPrice => Price * Quantity;

    }
}
