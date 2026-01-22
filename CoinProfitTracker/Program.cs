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
if (bid > ask) Console.WriteLine("Warning: the bid is higher than ask; results and calculations may be off.");

// produce formula to break even, including fees
decimal breakEvenBid = ask * (1 + tradingFee) / (1 - tradingFee);

// calculate break-even price
decimal percentageToBreakEven = (breakEvenBid - bid)/bid * 100;
Console.WriteLine();
Console.WriteLine($"To break even, the bid must rise by {percentageToBreakEven:F2}% to ${breakEvenBid:F2}");

// calculate different profit points
decimal onePercentProfitBid = GetBidForPercentProfit(0.01m, breakEvenBid);
decimal percentageToOnePercentProfit = GetPercentageToBid(onePercentProfitBid, bid);
Console.WriteLine($"For a 1% profit, the bid must rise by {percentageToOnePercentProfit:F2}% to ${onePercentProfitBid:F2}");

decimal fivePercentProfitBid = GetBidForPercentProfit(0.05m, breakEvenBid);
decimal percentageToFivePercentProfit = GetPercentageToBid(fivePercentProfitBid, bid);
Console.WriteLine($"For a 5% profit, the bid must rise by {percentageToFivePercentProfit:F2}% to ${fivePercentProfitBid:F2}");

decimal tenPercentProfitBid = GetBidForPercentProfit(0.1m, breakEvenBid);
decimal percentageToTenPercentProfit = GetPercentageToBid(tenPercentProfitBid, bid);
Console.WriteLine($"For a 10% profit, the bid must rise by {percentageToTenPercentProfit:F2}% to ${tenPercentProfitBid:F2}");


// I want to send to the frontend: percentages to 1/5/10% profit & 1/5/10$ profit bids; so 6 values in total.

decimal GetBidForPercentProfit(decimal targetProfitDecimal, decimal baseBid)
{
    return baseBid * (1 + targetProfitDecimal);
}

decimal GetPercentageToBid(decimal targetBid, decimal baseBid)
{
    return (targetBid - baseBid)/baseBid * 100;
}