using CommunityToolkit.Mvvm.ComponentModel;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.ViewModels.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.ViewModels.StockPurchases
{
    public class StockPurchaseProductViewModel : ObservableObject
    {
        public StockPurchaseProductViewModel(IStockPurchaseProduct stockPurchaseProduct)
        {
            _stockPurchaseProduct = stockPurchaseProduct;
        }

        private readonly IStockPurchaseProduct _stockPurchaseProduct;

        public Guid ProductId => _stockPurchaseProduct.ProductId;

        public string Name => _stockPurchaseProduct.Name;

        public string Barcode => _stockPurchaseProduct.Barcode;

        public decimal Price => _stockPurchaseProduct.Price;

        public int Quantity => _stockPurchaseProduct.QuantityBought;

        public decimal TotalPrice => Price * Quantity;

    }
}
