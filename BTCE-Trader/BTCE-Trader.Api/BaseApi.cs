﻿using System.Collections.Generic;
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

        protected string V3Query(string methodName, string parameters)
        {
            return webRequest.RequestV3(methodName, parameters);
        }

    }
}
