namespace ExchangeRates.Domain;

public interface IExchangeRateService
{
    Task SaveExchageRateAsync(ExchangeRate exchangeRate);

    Task<ExchangeRate> GetCurrentExchageRateAsync(string code);

    Task<IEnumerable<Currency>> GetCurrenciesAsync();
}