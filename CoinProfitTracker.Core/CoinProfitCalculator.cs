namespace CoinProfitTracker.Core
{
    public class CoinProfitCalculator
    {
        private const decimal TradingFee = 0.001m;
        public TradeMetrics CalculateProfits(decimal ask, decimal bid)
        {
            // produce formula to break even, including fees
            decimal breakEvenBid = ask * (1 + TradingFee) / (1 - TradingFee);

            // calculate break even price
            decimal percentageToBreakEven = (breakEvenBid - bid) / bid * 100;

            // calculate different profit points
            decimal onePercentProfitBid = GetBidForPercentProfit(0.01m, breakEvenBid);
            decimal percentageToOnePercentProfit = GetPercentageToBid(onePercentProfitBid, bid);
            // Console.WriteLine($"For a 1% profit, the bid must rise by {percentageToOnePercentProfit:F2}% to ${onePercentProfitBid:F2}");

            decimal fivePercentProfitBid = GetBidForPercentProfit(0.05m, breakEvenBid);
            decimal percentageToFivePercentProfit = GetPercentageToBid(fivePercentProfitBid, bid);
            // Console.WriteLine($"For a 5% profit, the bid must rise by {percentageToFivePercentProfit:F2}% to ${fivePercentProfitBid:F2}");

            decimal tenPercentProfitBid = GetBidForPercentProfit(0.1m, breakEvenBid);
            decimal percentageToTenPercentProfit = GetPercentageToBid(tenPercentProfitBid, bid);
            // Console.WriteLine($"For a 10% profit, the bid must rise by {percentageToTenPercentProfit:F2}% to ${tenPercentProfitBid:F2}");
            return new TradeMetrics(ask, bid, percentageToOnePercentProfit, onePercentProfitBid, percentageToFivePercentProfit, fivePercentProfitBid, percentageToTenPercentProfit, tenPercentProfitBid);
        }

        // I want to send to the frontend: percentages to 1/5/10% profit & 1/5/10$ profit bids; so 6 values in total.

        private decimal GetBidForPercentProfit(decimal targetProfitDecimal, decimal baseBid)
        {
            return baseBid * (1 + targetProfitDecimal);
        }

        private decimal GetPercentageToBid(decimal targetBid, decimal baseBid)
        {
            return (targetBid - baseBid) / baseBid * 100;
        }
    }
}
