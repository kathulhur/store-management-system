using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;

namespace StoreManagementSystemX.Domain.Factories.Transactions.Interfaces
{
    public interface ITransactionReconstitutionArgs
    {
        public Guid SellerId { get; }

        public DateTime DateTime { get; }

        public Guid Id { get; }

        public IPayLaterReconstitutionArgs? PayLater { get; }

        public IEnumerable<ITransactionProductReconstitutionArgs> TransactionProducts { get; }
    }
}