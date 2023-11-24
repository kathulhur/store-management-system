using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.StockPurchases.Interfaces
{
    public interface ICreateStockPurchaseProductViewModel
    {
        public string ProductBarcode { get; }

        public Product Product { get; }

        public StockPurchase StockPurchase { get; }


        public string ProductName { get; }

        public int Quantity { get; set; }

        public decimal Price { get; }

        public decimal Subtotal { get; }

        public ICommand RemoveCommand { get; }

        public ICommand IncrementQuantityCommand { get; }

        public ICommand DecrementQuantityCommand { get; }

        public StockPurchaseProduct BuildStockPurchaseProduct();
    }
}
