using ExchangeRates.Domain;
using ExchangeRates.Implementation.DataObjects;
using Microsoft.AspNetCore.Mvc;


namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRateService exchangeRateService;

        public ExchangeRatesController(IExchangeRateService exchangeRateService)
        {
            this.exchangeRateService = exchangeRateService;
        }

        [HttpGet("{code}")]
        public async Task<ExchangeRateDTO> GetExchangeRateAsync(string code)
        {
            var exchangeRates = await exchangeRateService.GetCurrentExchageRateAsync(code);

            return new ExchangeRateDTO
            {
                Code = exchangeRates.Code,
                Currency = exchangeRates.Currency,
                ExchangeRate = exchangeRates.Mid
            };
        }

        [HttpGet("currencies")]
        public async Task<IEnumerable<CurrencyDTO>> GetCurrenciesAsync()
        {
            var currencies = await exchangeRateService.GetCurrenciesAsync();

            return currencies.Select(x => new CurrencyDTO
            {
                Code = x.Code,
                Currency = x.Name
            });
        }
    }
}
