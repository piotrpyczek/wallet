using Common.Exceptions;
using Wallet.Domain.Infrastructure;

namespace Wallet.Domain.Entities;

public class CurrencyBucket : Entity
{
    public string CurrencyCode { get; set; }

    public decimal Amount { get; set; }

    private List<Transaction> transactions;
    public ICollection<Transaction> Transactions => transactions.AsReadOnly();

    public CurrencyBucket()
    {
        transactions = new List<Transaction>();
    }

    public void ApplyTransaction(Transaction transaction)
    {
        // transaction.Amount moze byc ujemna
        if (Amount + transaction.Amount < 0)
        {
            throw new BadRequestException("Insufficient funds");
        }

        Amount += transaction.Amount;
        transactions.Add(transaction);
    }
}