using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using denis.GoRestSpecFlow.Core.Config;
using denis.GoRestSpecFlow.Core.ContextContainers;
using denis.GoRestSpecFlow.Core.Helpers;
using TechTalk.SpecFlow.Infrastructure;

namespace denis.GoRestSpecFlow.Core.Support
{
    [Binding]
    public class Hooks
    {
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        private TestContextContainer _testContext;
        private readonly BaseConfig _baseConfig;

        public Hooks(ISpecFlowOutputHelper specFlowOutputHelper, TestContextContainer testContext, BaseConfig baseConfig)
        {
            _specFlowOutputHelper = specFlowOutputHelper;
            _testContext = testContext;
            _baseConfig = baseConfig;
        }

        [BeforeScenario(Order = 1)]
        public void TearUp()
        {
            _testContext.HttpClient = new HttpClient();
        }

        [BeforeScenario("Authenticate", Order = 2)]
        public void Authenticate()
        {
            _testContext.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }

        [AfterScenario]
        public async Task TearDown()
        {
            if (_testContext.CreatedUser is null)
            {
                return;
            }
            var message = HttpMessageHelper.CreateRequestMessage($"{_testContext.CreatedUser.Id}", HttpMethod.Delete, "{}");
            var result = await _testContext.HttpClient.SendAsync(message);

            if (!result.IsSuccessStatusCode)
            {
                _specFlowOutputHelper.WriteLine("Delete failed.");
            }
        }
    }
}
