using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.Api.Web;

namespace BTCE_Trader.Api
{
    public class BaseApi
    {        
        private IWebRequestWrapper webRequest { get; set; }

        public BaseApi(IWebRequestWrapper webRequest)
        {
            this.webRequest = webRequest;
        }

        protected string Query(string methodName, Dictionary<string, string> args)
        {
            return webRequest.RequestData(methodName, args);
        }

        protected string Query(string methodName)
        {
            return  webRequest.RequestData(methodName, new Dictionary<string, string>());
        }
        

    }
}
