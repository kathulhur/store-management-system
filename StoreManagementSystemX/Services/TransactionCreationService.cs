using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using StoreManagementSystemX.Views.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Services
{
    public class TransactionCreationService : ITransactionCreationService
    {

        public TransactionCreationService(
            Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository, 
            Domain.Repositories.Products.Interfaces.IProductRepository productRepository, 
            IDialogService dialogService)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
            _dialogService = dialogService;
        }

        private readonly Domain.Repositories.Transactions.Interfaces.ITransactionRepository _transactionRepository;
        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;

        private readonly IDialogService _dialogService;

        public Guid? CreateNewTransaction(AuthContext authContext)
        {
            Guid? newTransaction = null;
            var window = new CreateTransactionWindow(authContext, _transactionRepository, _productRepository, _dialogService, (Guid transactionId) =>
            {
                newTransaction = transactionId;
            });

            window.ShowDialog();
            return newTransaction;

        }
    }
}
