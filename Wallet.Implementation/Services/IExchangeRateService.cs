using Wallet.Implementation.DataObjects;

namespace Wallet.Implementation.Services
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateDTO> GetExchangeRateAsync(string currencyCode, CancellationToken cancellationToken = default);
    }
}
