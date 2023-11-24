
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Views.Transactions;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class TransactionProductRowViewModel : ITransactionProductRowViewModel
    {
        public TransactionProductRowViewModel(TransactionProduct transactionProduct)
        {
            _transactionProduct = transactionProduct;
        }

        private readonly TransactionProduct _transactionProduct;

        public string ProductName => _transactionProduct.ProductName;

        public int Quantity => _transactionProduct.QuantityBought;

        public decimal SellingPrice => _transactionProduct.PriceSold;

        public decimal TotalCost => Quantity * _transactionProduct.CostPrice;

        public decimal TotalPrice => Quantity * SellingPrice;
    }
}