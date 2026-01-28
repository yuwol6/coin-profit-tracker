using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinProfitTracker.Core
{
    public record TradeMetrics(
        decimal Ask,
        decimal Bid,
        decimal PercentageToOnePercentProfit,
        decimal OnePercentProfitBid,
        decimal PercentageToFivePercentProfit,
        decimal FivePercentProfitBid,
        decimal PercentageToTenPercentProfit,
        decimal TenPercentProfitBid
        );
}
