using System.Collections.Generic;

namespace BTCE_Trader.Api.Web
{
    public interface IWebRequestWrapper
    {
        string RequestData(string method, Dictionary<string, string> postArguments);
        string RequestV3(string method, string parameters);
    }
}
