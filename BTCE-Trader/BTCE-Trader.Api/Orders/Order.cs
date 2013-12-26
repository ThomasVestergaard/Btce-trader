using System;

namespace BTCE_Trader.Api.Orders
{
    public class Order : IOrder
    {
        private const string decimalFormat = "0.00000000";
        public string Id { get; set; }
        public BtcePairEnum Pair { get; set; }
        public TradeTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        
        public string Summery
        {
            get { return string.Format("{0} {1} {2} @ {3}", 
                Type,
                Amount.ToString(decimalFormat),
                Pair,
                Rate.ToString(decimalFormat)); 
            }
        }

    }
}
