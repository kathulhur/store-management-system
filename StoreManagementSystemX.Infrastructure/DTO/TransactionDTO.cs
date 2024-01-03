using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Infrastructure.DTO
{
    public class TransactionDTO : ITransactionReconstitutionArgs
    {
        public TransactionDTO(TransactionDBModel transaction, PayLaterDTO? payLater, IEnumerable<TransactionProductDTO> transactionProducts)
        {
            Id = transaction.Id;
            SellerId = transaction.SellerId;
            DateTime = transaction.DateTime;
            PayLater = payLater;
            TransactionProducts = transactionProducts;
        }

        public Guid SellerId { get; set; }

        public DateTime DateTime { get; set; }

        public Guid Id { get; set; }

        public IPayLaterReconstitutionArgs? PayLater { get; set; } = null!;

        public IEnumerable<ITransactionProductReconstitutionArgs> TransactionProducts { get; }

    }
}
