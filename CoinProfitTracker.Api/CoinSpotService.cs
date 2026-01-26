using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace CoinProfitTracker.Api
{
    public class CoinSpotService
    {
        // pulls API and sets initial constants
        private readonly HttpClient _http;
        private readonly ILogger<CoinSpotService> _logger;
        const string bitCoin = "BTC";
        const decimal tradingFee = 0.001m;
        private readonly string _url;

        public CoinSpotService(HttpClient http, ILogger<CoinSpotService> logger)
        {
            _http = http;
            _logger = logger;
            _url = $"https://www.coinspot.com.au/pubapi/v2/latest/{bitCoin}";
        }

        public async Task<(decimal ask, decimal bid)> GetPricesAsync()
        {
            string jsonText = await _http.GetStringAsync(_url);

            using JsonDocument doc = JsonDocument.Parse(jsonText);
            JsonElement root = doc.RootElement;

            string status = root.GetProperty("status").GetString()!;
            if (status != "ok")
            {
                throw new InvalidOperationException("API request failed.");
            }

            JsonElement prices = root.GetProperty("prices"); // get ask and bid as decimal variable
            decimal ask = decimal.Parse(prices.GetProperty("ask").GetString()!, CultureInfo.InvariantCulture);
            decimal bid = decimal.Parse(prices.GetProperty("bid").GetString()!, CultureInfo.InvariantCulture);
            return (ask, bid);
        }
        
    }
}
