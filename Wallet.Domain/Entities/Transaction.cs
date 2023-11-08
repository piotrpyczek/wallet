using Wallet.Domain.Enums;
using Wallet.Domain.Infrastructure;

namespace Wallet.Domain.Entities;

public class Transaction : Entity
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public TransactionStatus Status { get; set; }

    public Guid? ReferencedTransactionId { get; set; }
    public Transaction? ReferencedTransaction { get; set; }
}