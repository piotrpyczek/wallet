using MediatR;

namespace Wallet.Domain.Messaging;

public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public abstract Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand, IRequest
{
    public abstract Task Handle(TCommand command, CancellationToken cancellationToken);
}