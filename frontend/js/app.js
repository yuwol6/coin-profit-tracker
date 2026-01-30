const refreshBtn = document.getElementById('refreshBtn');
const askText = document.getElementById('ask-text');
const bidText = document.getElementById('bid-text');
getTradeMetrics();

async function getTradeMetrics() {
  const metricResponse = await fetch('https://localhost:7297/api/prices');
  const metricData = await metricResponse.json();

  console.log(metricData);

  displayMetrics(metricData);
}

function displayMetrics(metrics) {
  const askMetrics = metrics.ask.toLocaleString('en-AU', {
    style: 'currency',
    currency: 'AUD',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });
  const bidMetrics = metrics.bid.toLocaleString('en-AU', {
    style: 'currency',
    currency: 'AUD',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  });

  askText.textContent = askMetrics;
  bidText.textContent = bidMetrics;
}

refreshBtn.addEventListener('click', () => {
  getTradeMetrics();
});
