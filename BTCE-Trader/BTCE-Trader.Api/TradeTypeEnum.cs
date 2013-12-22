using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api
{
    public enum TradeTypeEnum
    {
        Sell,
        Buy
    }

    public class TradeTypeHelper
    {
        public static TradeTypeEnum FromString(string s)
        {
            switch (s)
            {
                case "sell":
                    return TradeTypeEnum.Sell;
                case "buy":
                    return TradeTypeEnum.Buy;
                default:
                    throw new ArgumentException();
            }
        }
        public static string ToString(TradeTypeEnum v)
        {
            return Enum.GetName(typeof(TradeTypeEnum), v).ToLowerInvariant();
        }
    }
}
