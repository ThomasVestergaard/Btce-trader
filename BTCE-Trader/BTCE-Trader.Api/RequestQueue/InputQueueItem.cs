using System;
using System.Collections.Generic;

namespace BTCE_Trader.Api.RequestQueue
{
    public class InputQueueItem
    {
        public Guid RequestId { get; set; }
        public string MethodName { get; set; }
        public Dictionary<string, string> MethodParameters { get; set; }

    }
}
