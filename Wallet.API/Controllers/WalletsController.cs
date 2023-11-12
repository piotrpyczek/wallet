using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Models;
using Wallet.Commands;
using Wallet.Commands.Wallet;
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
        public Task<IEnumerable<WalletDTO>> GetWalletsAsync(CancellationToken cancellationToken)
        {
            return mediator.Send(new GetWalletsQuery(), cancellationToken);
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

        [HttpPost("{walletId:guid}/payment")]
        public async Task PaymentIntoWalletAsync(Guid walletId, [FromBody] PaymentModel model, CancellationToken cancellationToken)
        {
            var command = new PaymentIntoWalletCommand(walletId, model.CurrencyCode, model.Amount);

            logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetType().Name,
                nameof(command.WalletId),
                command.WalletId,
                command);

            await mediator.Send(command, cancellationToken);
        }

        [HttpPost("{walletId:guid}/withdraw")]
        public async Task WithdrawFromWalletAsync(Guid walletId, [FromBody] WithdrawModel model, CancellationToken cancellationToken)
        {
            var command = new WithdrawFromWalletCommand(walletId, model.CurrencyCode, model.Amount);

            logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetType().Name,
                nameof(command.WalletId),
                command.WalletId,
                command);

            await mediator.Send(command, cancellationToken);
        }

        [HttpPost("{walletId:guid}/conversion")]
        public async Task DepositConversionWalletAsync(Guid walletId, [FromBody] DepositConversionModel model, CancellationToken cancellationToken)
        {
            var command = new DepositConversionCommand(walletId, model.CurrencyCodeFrom, model.CurrencyCodeTo, model.Amount);

            logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetType().Name,
                nameof(command.WalletId),
                command.WalletId,
                command);

            await mediator.Send(command, cancellationToken);
        }

    }
}
