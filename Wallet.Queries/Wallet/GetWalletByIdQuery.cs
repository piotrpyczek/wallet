using Wallet.Domain.Messaging;
using Wallet.Implementation.DataObjects;

namespace Wallet.Queries;

public class GetWalletByIdQuery : IQuery<WalletDTO>
{
    public GetWalletByIdQuery(Guid? id)
    {
        Id = id;
    }

    public Guid? Id { get; }
}