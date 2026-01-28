async function testBackend() {
  const response = await fetch('https://localhost:7297/api/ping');
  const data = await response.json();

  console.log('Backend says:', data);
}

async function getBackend() {
  const metricResponse = await fetch('https://localhost:7297/api/Prices');
  const metricData = await metricResponse.json();

  console.log('The metrics are:', metricData);
}

testBackend();
getBackend();
