using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace denis.GoRestSpecFlow.Core.Config
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("testconfig.json")
                .Build();

            HttpClientConfig = config.GetSection("HttpClient").Get<HttpClientConfig>();
        }

        public HttpClientConfig HttpClientConfig { get; set; }
    }
}
