# CoinProfitTracker

A small C# project that fetches CoinSpot cryptocurrency prices and calculates trading profit transition points, using a REST API and JSON parsing.

The goal of this project is to simplify and automate the answer to the question, "If I buy now, at what price should I sell to avoid losses and maximise profit?"

This tracker is designed for highly liquid assets like BTC, where spreads tend to be stable. Assuming similar spreads at entry and exit is a reasonable simplification under normal market conditions and keeps the calculations deterministic.

## Planned Features

- A lightend front-end interface to visualise profit transition points and price data.
