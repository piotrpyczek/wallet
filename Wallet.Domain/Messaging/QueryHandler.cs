namespace Wallet.Domain.Messaging
{
  public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
  {
    public abstract Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
  }
}
