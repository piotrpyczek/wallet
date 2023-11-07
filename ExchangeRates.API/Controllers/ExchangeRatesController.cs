using ExchangeRates.Domain;
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
        public async Task<ExchangeRate> GetExchangeRateAsync(string code)
        {
            return await exchangeRateService.GetCurrentExchageRateAsync(code);
        }
    }
}
