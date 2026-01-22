console.log('JS is running');

async function testBackend() {
  const response = await fetch('https://localhost:7297/api/ping');
  const data = await response.json();

  console.log('Backend says:', data);
}

testBackend();
