using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoinProfitTracker.Core;

namespace CoinProfitTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly CoinSpotService _coinSpot;
        private readonly CoinProfitCalculator _calculator;
        public PricesController(CoinSpotService coinSpot, CoinProfitCalculator calculator)
        {
            _coinSpot = coinSpot;
            _calculator = calculator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrices()
        {
            var (ask, bid) = await _coinSpot.GetPricesAsync();

            TradeMetrics metrics = _calculator.CalculateProfits(ask, bid);

            return Ok(metrics);
        }
    }
}
