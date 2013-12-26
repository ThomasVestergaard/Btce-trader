This project contains two sub projects:
1. Complete Btc-e API C# wrapper.
2. C# WPF userinterface to handle trades through the API wrapper.



TODO:
- Btce API. Support following:
   - Get active order. (done)
   - Get depth on pairs (done)
   - Get latest trades on pairs
   - Post order on exchange
   - Cancel active order (done)
   - Get account info (In progress)

- UI (C# WPF MVVM)
   - Display basic order depth (done)
   - Format order depth nicely (done)
   - Add main window with ribbon (done)
   - Add avalon dock
   - Display aggregated order depth (done)
   - Display active orders (done)
   - Cancel active orders (done)
   - Place orders
   - Show account info

External libraries used:
   - Newtonsoft.Json for jSon serialization
   - Castle windsor for dependency injection
   - NUnit for unit testing
   - Rhinomocks for mocking
   - Avalon dock for UI


BTCE-TRADER - C# WPF TRADING PLATFORM
=====================================
Current version: 0.000001a

The Btce-trader is a C# WPF project which wraps basic trading functionality for the btc-e exchange.
