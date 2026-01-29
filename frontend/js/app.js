async function getBackend() {
  const metricResponse = await fetch('https://localhost:7297/api/prices');
  const metricData = await metricResponse.json();

  console.log('The metrics are:', metricData);
}

getBackend();
