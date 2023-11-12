using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Messaging;
using Wallet.Implementation;
using Wallet.Implementation.Converters;
using Wallet.Implementation.DataObjects;

namespace Wallet.Queries.Wallet.Handlers;

public class GetWalletsQueryHandler : QueryHandler<GetWalletsQuery, IEnumerable<WalletDTO>>
{
    private readonly WalletDbContext context;

    public GetWalletsQueryHandler(WalletDbContext context)
    {
        this.context = context;
    }

    public override async Task<IEnumerable<WalletDTO>> Handle(GetWalletsQuery query, CancellationToken cancellationToken)
    {
        var wallets = await context.Wallets
            .AsNoTracking()
            .Include(x => x.Buckets)
            .ToListAsync(cancellationToken);

        return wallets.Select(wallet => wallet.ToWalletDTO());
    }
}