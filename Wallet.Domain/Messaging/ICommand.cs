using MediatR;

namespace Wallet.Domain.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

public interface ICommand : IRequest
{
}