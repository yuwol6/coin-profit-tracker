using System.Net.Http;
using System.Text.Json;
using System.Globalization;


// pulls API and sets initial constants
const string bitCoin = "BTC";
const decimal tradingFee = 0.001m;
string url = $"https://www.coinspot.com.au/pubapi/v2/latest/{bitCoin}";

using var http = new HttpClient();
string jsonText = await http.GetStringAsync(url);

Console.WriteLine("Calling CoinSpot API...");
Console.WriteLine();

using JsonDocument doc = JsonDocument.Parse(jsonText);
JsonElement root = doc.RootElement;

// check the status
string status = root.GetProperty("status").GetString()!;
if (status != "ok")
{
    Console.WriteLine("API request failed.");
    return;
}

JsonElement prices = root.GetProperty("prices"); // get ask and bid as decimal variable

decimal ask = decimal.Parse(prices.GetProperty("ask").GetString()!, CultureInfo.InvariantCulture);
Console.WriteLine($"The ask (buying price) is: ${ask:F2}");
decimal bid = decimal.Parse(prices.GetProperty("bid").GetString()!, CultureInfo.InvariantCulture);
Console.WriteLine($"The bid (selling price) is: ${bid:F2}");

// produce formula to break even, including fees
decimal breakEvenBid = ask * (1 + tradingFee) / (1 - tradingFee);

// calculate break-even price
decimal differencePercentage = (breakEvenBid - bid)/bid * 100;
Console.WriteLine();
Console.WriteLine($"To break even, the bid must rise {differencePercentage:F2}% to ${breakEvenBid:F2}");