using System.Text.Json;
using Common;
using Common.Exceptions;
using Wallet.Implementation.DataObjects;

namespace Wallet.Implementation.Services;

public class ExchangeRateService : IExchangeRateService
{
    private readonly HttpClient client;

    public ExchangeRateService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ExchangeRateDTO> GetExchangeRateAsync(string currencyCode, CancellationToken cancellationToken)
    {
        var uri = $"api/ExchangeRates/{currencyCode}";
        var response = await client.GetAsync(uri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.GetErrorDetailsAsync(cancellationToken);
            throw new BadRequestException(error.Detail);
        }

        var exhangeRateResponse = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<ExchangeRateDTO>(exhangeRateResponse, JsonDefaults.CaseInsensitiveOptions)!;
    }

    public async Task<IEnumerable<CurrencyDTO>> GetCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        var uri = $"api/ExchangeRates/currencies";
        var response = await client.GetAsync(uri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.GetErrorDetailsAsync(cancellationToken);
            throw new BadRequestException(error.Detail);
        }

        var exhangeRateResponse = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<IEnumerable<CurrencyDTO>>(exhangeRateResponse, JsonDefaults.CaseInsensitiveOptions)!;
    }
}