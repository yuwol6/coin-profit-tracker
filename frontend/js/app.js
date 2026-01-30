const lastRefreshText = document.getElementById('last-refresh-text');
const refreshBtn = document.getElementById('refreshBtn');
const askText = document.getElementById('ask-text');
const bidText = document.getElementById('bid-text');
const oneRisePercent = document.getElementById('one-rise-percent');
const fiveRisePercent = document.getElementById('five-rise-percent');
const tenRisePercent = document.getElementById('ten-rise-percent');
const oneProfitBid = document.getElementById('one-profit-bid');
const fiveProfitBid = document.getElementById('five-profit-bid');
const tenProfitBid = document.getElementById('ten-profit-bid');
getTradeMetrics();

async function getTradeMetrics() {
  const metricResponse = await fetch('https://localhost:7297/api/prices');
  const metricData = await metricResponse.json();

  displayTime();
  displayMetrics(metricData);
}

function displayMetrics(metrics) {
  const askMetrics = changeNumToAud(metrics.ask);
  const bidMetrics = changeNumToAud(metrics.bid);
  const oneProfitMetrics = changeNumToAud(metrics.onePercentProfitBid);
  const fiveProfitMetrics = changeNumToAud(metrics.fivePercentProfitBid);
  const tenProfitMetrics = changeNumToAud(metrics.tenPercentProfitBid);

  askText.textContent = askMetrics;
  bidText.textContent = bidMetrics;

  oneRisePercent.textContent = `${metrics.percentageToOnePercentProfit.toFixed(2)}%`;
  fiveRisePercent.textContent = `${metrics.percentageToFivePercentProfit.toFixed(2)}%`;
  tenRisePercent.textContent = `${metrics.percentageToTenPercentProfit.toFixed(2)}%`;

  oneProfitBid.textContent = oneProfitMetrics;
  fiveProfitBid.textContent = fiveProfitMetrics;
  tenProfitBid.textContent = tenProfitMetrics;
}

function changeNumToAud(number) {
  const returnNum = number.toLocaleString('en-AU', {
    style: 'currency',
    currency: 'AUD',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });

  return returnNum;
}

function displayTime() {
  const timeNow = new Date().toLocaleTimeString('en-AU', {
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
  });
  lastRefreshText.textContent = timeNow;
}

refreshBtn.addEventListener('click', () => {
  getTradeMetrics();
});
