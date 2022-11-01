using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using denis.GoRestSpecFlow.Core.Config;

namespace denis.GoRestSpecFlow.Core.Helpers
{
    public static class HttpMessageHelper
    {
        private static BaseConfig baseConfig = new BaseConfig();
        public static HttpRequestMessage CreateRequestMessage(string endpoint, HttpMethod httpMethod, string content)
        {
            return new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri($"{baseConfig.HttpClientConfig.BaseUrl}{endpoint}"),
                Content = new StringContent(content, encoding: Encoding.UTF8, mediaType: "application/json")
            };
        }

    }
}
