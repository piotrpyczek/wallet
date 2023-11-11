using Common.Exceptions;
using Wallet.Domain.Infrastructure;

namespace Wallet.Domain.Entities;

public class CurrencyBucket : Entity
{
    public string CurrencyCode { get; set; }

    public decimal Amount { get; set; }

    public ICollection<Transaction> Transactions { get; set; }

    public void ApplyTransaction(Transaction transaction)
    {
        // transaction.Amount moze byc ujemna
        if (Amount + transaction.Amount < 0)
        {
            throw new BadRequestException("Niewystarczająca ilość środków");
        }

        Amount += transaction.Amount; 
        Transactions.Add(transaction);
    }
}