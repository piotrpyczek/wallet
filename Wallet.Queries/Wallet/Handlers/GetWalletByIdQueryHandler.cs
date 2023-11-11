using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Messaging;
using Wallet.Implementation;
using Wallet.Implementation.Converters;
using Wallet.Implementation.DataObjects;

namespace Wallet.Queries.Wallet.Handlers;

public class GetWalletByIdQueryHandler : QueryHandler<GetWalletByIdQuery, WalletDTO>
{
    private readonly WalletDbContext context;

    public GetWalletByIdQueryHandler(WalletDbContext context)
    {
        this.context = context;
    }

    public override async Task<WalletDTO> Handle(GetWalletByIdQuery query, CancellationToken cancellationToken)
    {
        var wallet = await context.Wallets
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

        if (wallet == null)
        {
            throw new NotFoundException();
        }

        return wallet.ToWalletDTO();
    }
}