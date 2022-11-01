using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using denis.GoRestSpecFlow.Core.ContextContainers;
using denis.GoRestSpecFlow.Models;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace denis.GoRestSpecFlow.Tests.StepDefinitions
{
    [Binding]
    public class UsersAssertStepDefinitions
    {
        private readonly TestContextContainer _contextContainer;

        public UsersAssertStepDefinitions(TestContextContainer contextContainer)
        {
            _contextContainer = contextContainer;
        }
        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBeCreated(string statusCode)
        {
            if (_contextContainer.IsValidUser)
            {                
                _contextContainer.ResponseMessage.StatusCode.ToString().Should().Be(statusCode);
                return;
            }

            _contextContainer.ResponseMessage.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"the response content should be valid")]
        public async Task ThenTheResponseContentShouldBeValid()
        {
            if (_contextContainer.IsValidUser)
            {
                var validResponse = await _contextContainer.ResponseMessage.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<GoRestUser>(validResponse);
                using (new AssertionScope())
                {
                    user.Id.Should().NotBe(null);
                    user.Name.Should().Be(_contextContainer.ToBeCreatedUser.Name);
                    user.Email.Should().Be(_contextContainer.ToBeCreatedUser.Email);
                    user.Gender.Should().Be(_contextContainer.ToBeCreatedUser.Gender.ToLower());
                    user.Status.Should().Be(_contextContainer.ToBeCreatedUser.Status.ToLower());
                }

                return;
            }

            var errorResponse = await _contextContainer.ResponseMessage.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<IEnumerable<ErrorModel>>(errorResponse).First();

            error.Message.Should().Be("can't be blank");
        }

        [Then(@"The response should be (.*)")]
        public void ThenTheResponseShouldBeOK(string statusCode)
        {
            _contextContainer.ResponseMessage.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"the content should not be empty")]
        public async Task ThenTheContentShouldNotBeEmpty()
        {
            var content = await _contextContainer.ResponseMessage.Content.ReadAsStringAsync();
            var contentObjects = JsonConvert.DeserializeObject<IEnumerable<GoRestUser>>(content);
            contentObjects.Should().NotBeEmpty();
        }

        [Then(@"the user should be updated successfully")]
        public async Task ThenTheUserShouldBeUpdatedSuccessfully()
        {
            var validResponse = await _contextContainer.ResponseMessage.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<GoRestUser>(validResponse);
            using (new AssertionScope())
            {
                user.Id.Should().Be(_contextContainer.CreatedUser.Id);
                user.Name.Should().Be(_contextContainer.CreatedUser.Name);
                user.Email.Should().Be(_contextContainer.CreatedUser.Email);
                user.Gender.Should().Be(_contextContainer.CreatedUser.Gender.ToLower());
                user.Status.Should().Be(_contextContainer.CreatedUser.Status.ToLower());
            }
        }

    }
}
