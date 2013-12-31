This project contains three sub projects:
1. Complete Btc-e API C# wrapper.
2. Async API wrapper for easy integration into every type of .NET application.
3. C# WPF userinterface to handle trades through the API wrapper.


TODO:
- Btce API. Support following:
   - Get active order. (done)
   - Get depth on pairs (done)
   - Get latest trades on pairs
   - Post order on exchange (done)
   - Cancel active order (done)
   - Get account info (done)

- Async wrapper
   - Basic threaded agents for all API functions (done)
   - Make queued webrequest wrapper due to btc-e's noonce security thingy (in progress)


- UI (C# WPF MVVM)
   - Display basic order depth (done)
   - Format order depth nicely (done)
   - Add main window with ribbon (done)
   - Add avalon dock
   - Display aggregated order depth (done)
   - Display active orders (done)
   - Cancel active orders (done)
   - Place orders via order depth control (done)
   - Show account info (done)

External libraries used:
   - Newtonsoft.Json for jSon serialization
   - Castle windsor for dependency injection
   - NUnit for unit testing
   - Rhinomocks for mocking
   - Avalon dock for UI
   - Disrupter-NET for cross thread message queue
   - Log4Net for logging


BTCE-TRADER - C# WPF TRADING PLATFORM
=====================================
Current version: 0.8a

The Btce-trader is a C# WPF project which wraps basic trading functionality for the btc-e exchange.
