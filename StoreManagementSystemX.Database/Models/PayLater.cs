using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace StoreManagementSystemX.Database.Models
{
    public class PayLater
    {
        public Guid Id { get; set; }
        
        public Guid TransactionId { get; set; }

        public string CustomerName { get; set; } = String.Empty;

        public Transaction Transaction { get; set; } = null!;

        public bool IsPaid { get; set; }

        public DateTime? PaidAt { get; set; }

    }
}