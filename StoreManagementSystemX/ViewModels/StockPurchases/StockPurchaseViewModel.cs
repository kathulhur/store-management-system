using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Query;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.ViewModels.StockPurchases
{
    public class StockPurchaseViewModel : ObservableObject
    {
        public StockPurchaseViewModel(IStockPurchase stockPurchase)
        {
            _stockPurchase = stockPurchase;
        }

        private readonly IStockPurchase _stockPurchase;

        public Guid Id => _stockPurchase.Id;

        public DateTime DateTime => _stockPurchase.DateTime;

        public IEnumerable<StockPurchaseProductViewModel> StockPurchaseProducts => _stockPurchase.StockPurchaseProducts.Select(e => new StockPurchaseProductViewModel(e));

        public decimal TotalCost => StockPurchaseProducts.Sum(e => e.TotalPrice);

    }
}
