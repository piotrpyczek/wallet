using MediatR;

namespace Wallet.Domain.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}