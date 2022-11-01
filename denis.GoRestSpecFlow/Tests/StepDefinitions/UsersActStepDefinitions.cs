using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using denis.GoRestSpecFlow.Core.ContextContainers;
using denis.GoRestSpecFlow.Core.Helpers;
using Newtonsoft.Json;

namespace denis.GoRestSpecFlow.Tests.StepDefinitions
{
    [Binding]
    public class UsersActStepDefinitions
    {
        private readonly TestContextContainer _contextContainer;        

        public UsersActStepDefinitions(TestContextContainer contextContainer)
        {
            _contextContainer = contextContainer;
        }
        

        [Given(@"I have generated (.*) user data")]
        public void GivenIHaveGeneratedValidUserData(string userData)
        {
            if (userData == "invalid")
            {
                _contextContainer.IsValidUser = false;
            _contextContainer.ToBeCreatedUser = CreateFakeUser.GenerateInvalidPostUser();
                return;
            }
            _contextContainer.IsValidUser = true;
            _contextContainer.ToBeCreatedUser = CreateFakeUser.GenerateValidPostUser();
        }


        [When(@"I send a request to the users endpoint")]
        public async Task WhenISendARequestToTheUsersEndpoint()
        {
            _contextContainer.ResponseMessage = await _contextContainer.HttpClient.SendPostRequestAsync(_contextContainer.ToBeCreatedUser);
        }

        [Given(@"I want to get all users from the endpoint")]
        public void GivenIWantToGetAllUsersFromTheEndpoint()
        {
            
        }

        [When(@"I send a Get request to the users endpoint")]
        public async Task WhenISendAGetRequestToTheUsersEndpoint()
        {
            _contextContainer.ResponseMessage = await _contextContainer.HttpClient
                .SendAsync(HttpMessageHelper.CreateRequestMessage("", HttpMethod.Get, "{}"));
        }

        [Given(@"I have a created user")]
        public async Task GivenIHaveACreatedUser()
        {
            _contextContainer.CreatedUser = await _contextContainer.HttpClient.CreateUser();
        }

        [When(@"I send a PUT request to the users endpoint")]
        public async Task WhenISendAPUTRequestToTheUsersEndpoint()
        {

            var user = CreateFakeUser.GenerateValidPostUser();

            _contextContainer.CreatedUser.Name = user.Name;
            _contextContainer.CreatedUser.Gender = user.Gender;

            _contextContainer.ResponseMessage = await _contextContainer.HttpClient
                .SendAsync(HttpMessageHelper.CreateRequestMessage(_contextContainer.CreatedUser.Id.ToString(), HttpMethod.Put, JsonConvert.SerializeObject(_contextContainer.CreatedUser)));
        }     



    }
}
