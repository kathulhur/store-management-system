using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
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

        public TransactionCreationService(IUnitOfWorkFactory unitOfWorkFactory, IDialogService dialogService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _dialogService = dialogService;
        }

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IDialogService _dialogService;

        public Guid? CreateNewTransaction(AuthContext authContext)
        {
            Guid? newTransaction = null;
            using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var window = new CreateTransactionWindow(authContext, unitOfWork, _dialogService, (Guid transactionId) =>
                {
                    newTransaction = transactionId;
                });

                window.ShowDialog();


            }
            return newTransaction;

        }
    }
}
