using System.Collections.Generic;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.Configurations
{
    public class TradingConfigurations : ITradingConfigurations
    {
        public List<DefaultTradingSize> DefaultTradingSizes { get; private set; }

        public TradingConfigurations()
        {
            DefaultTradingSizes = new List<DefaultTradingSize>();
            SetDefaultTradingSizes();
        }

        public decimal CalculateAmount(BtcePairEnum pair, TradeTypeEnum tradeType, decimal CurrentHoldings)
        {
            decimal toReturn = 1;

            var tradeConf = DefaultTradingSizes.Find(a => a.Pair == pair && a.TradeType == tradeType);
            if (tradeConf == null)
                return toReturn;

            switch (tradeConf.TradeSizeType)
            {
                case DefaultTradingSizeType.FixedAmount:
                    toReturn = tradeConf.FixedAmount;
                    break;

                case DefaultTradingSizeType.PercentageOfCurrentHoldings:
                    toReturn = ((CurrentHoldings / 100) * tradeConf.PercentageOfCurrentHoldings);
                    break;
            }

            return toReturn;
        }

        private void SetDefaultTradingSizes()
        {
            #region ltc_usd
            DefaultTradingSizes.Add(new DefaultTradingSize
                {
                    Pair = BtcePairEnum.ltc_usd,
                    TradeType = TradeTypeEnum.Buy,
                    TradeSizeType = DefaultTradingSizeType.FixedAmount,
                    FixedAmount = 5,
                    PercentageOfCurrentHoldings = 0
                });

            DefaultTradingSizes.Add(new DefaultTradingSize
            {
                Pair = BtcePairEnum.ltc_usd,
                TradeType = TradeTypeEnum.Sell,
                TradeSizeType = DefaultTradingSizeType.PercentageOfCurrentHoldings,
                FixedAmount = 0,
                PercentageOfCurrentHoldings = 100
            });
            #endregion

            #region nmc_usd
            DefaultTradingSizes.Add(new DefaultTradingSize
            {
                Pair = BtcePairEnum.nmc_usd,
                TradeType = TradeTypeEnum.Buy,
                TradeSizeType = DefaultTradingSizeType.FixedAmount,
                FixedAmount = 10,
                PercentageOfCurrentHoldings = 0
            });

            DefaultTradingSizes.Add(new DefaultTradingSize
            {
                Pair = BtcePairEnum.nmc_usd,
                TradeType = TradeTypeEnum.Sell,
                TradeSizeType = DefaultTradingSizeType.PercentageOfCurrentHoldings,
                FixedAmount = 0,
                PercentageOfCurrentHoldings = 50
            });
            #endregion
        }
    }
}