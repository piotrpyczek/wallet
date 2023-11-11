using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Commands;
using Wallet.Implementation.DataObjects;
using Wallet.Queries;
using Wallet.Queries.Wallet;


namespace Wallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<WalletsController> logger;

        public WalletsController(IMediator mediator, ILogger<WalletsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<WalletDTO>> GetWalletsAsync([FromQuery] GetWalletsQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{walletId:guid}")]
        public Task<WalletDTO> GetWalletAsync(Guid walletId, CancellationToken cancellationToken)
        {
            return mediator.Send(new GetWalletByIdQuery(walletId), cancellationToken);
        }

        [HttpPost]
        public async Task<WalletDTO> CreateWalletAsync([FromBody] CreateWalletCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetType().Name,
                nameof(command.Name),
                command.Name,
                command);

            var walletId = await mediator.Send(command, cancellationToken);
            return await mediator.Send(new GetWalletByIdQuery(walletId), cancellationToken);
        }
    }
}
