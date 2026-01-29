# CoinProfitTracker

A web application project that fetches CoinSpot cryptocurrency prices and calculates trading profit transition points, using REST API, ASP.NET and JSON parsing.

The goal of this project is to simplify and automate the answer to the question, "If I buy now, at what price should I sell to avoid losses and maximise profit?" Using this web application, users do not have to calculate where the bid must rise to for their aimed profits.

This tracker is designed for highly liquid assets like BTC, where spreads tend to be stable. Assuming similar spreads at entry and exit is a reasonable simplification under normal market conditions and keeps the calculations deterministic.

The calculation logic involves fetching the bid and ask,

## Tech Stack

**Backend**

- C# / .NET 8
- ASP.NET Core Web API
- RESTful endpoints

**Frontend**

- HTML / CSS / JavaScript
- Fetch API
- Asynchronous data loading

## Architecture & Data Flow

The data required follows an atomic, layered architecture.
The "Refresh Prices" button in the web app starts the program process:

app.js
=> asynchronously requests data via HTTP GET request

CoinProfitTracker.Api
=> handles HTTP request, logging, external API call to CoinSpot API and request calculations from .Core

CoinProfitTracker.Core  
=> contains all calculation logic and returns metrics object to .Api

app.js
=> consumes the API and displays results in the browser

The Core project is framework-agnostic and contains no HTTP, logging, or UI code.
The Api project handles communication between the frontend, the Core project and external APIs.
A console app project was used to prototype the calculation logic and API communication.

## Running Locally

1. Clone the repository
2. Open the solution in Visual Studio
3. Set CoinProfitTracker.Api as the startup project
4. Run the API (F5)
5. Open the frontend HTML file in a browser (e.g. via Live Server Extension in VSCode)
6. Click the **Refresh Prices** button to fetch live calculations

Note: Update the base URL in app.js fetch() method to match your localhost address (e.g. localhost:xxxx).
