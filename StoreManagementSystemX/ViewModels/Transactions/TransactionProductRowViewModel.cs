
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Views.Transactions;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class TransactionProductRowViewModel : ITransactionProductRowViewModel
    {
        public TransactionProductRowViewModel(ITransactionProduct transactionProduct)
        {
            _transactionProduct = transactionProduct;
        }

        private readonly ITransactionProduct _transactionProduct;

        public string ProductName => _transactionProduct.ProductName;

        public int Quantity => _transactionProduct.QuantityBought;

        public decimal SellingPrice => _transactionProduct.SellingPrice;

        public decimal TotalCost => Quantity * _transactionProduct.CostPrice;

        public decimal TotalPrice => Quantity * SellingPrice;
    }
}