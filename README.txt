Btc-e api based on DmT021's C# wrapper. Please find it here: https://github.com/DmT021/BtceApi

TODO:
- Own btce API. Support following:
   - Get active order. (done)
   - Get depth on pairs (done)
   - Get latest trades on pairs
   - Post order on exchange
   - Cancel active order
   - Get account info

- UI (C# WPF MVVM)
   - Display basic order depth (done)
   - Format order depth nicely (done)
   - Add main window with ribbon (done)
   - Add avalon dock
   - Display aggregated order depth (done)
   - Display active orders (In progress - Buggy, concurrency issue)
   - Cancel active orders

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
