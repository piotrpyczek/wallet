using Wallet.Domain.Messaging;
using Wallet.Implementation.DataObjects;

namespace Wallet.Queries.Wallet;

public class GetWalletsQuery : IQuery<IEnumerable<WalletDTO>>
{
}