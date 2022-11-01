using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using denis.GoRestSpecFlow.Models;
using denis.GoRestSpecFlow.Models.Enums;
using Newtonsoft.Json;
using static Bogus.DataSets.Name;

namespace denis.GoRestSpecFlow.Core.Helpers
{
    public static class CreateFakeUser
    {        
        public static GoRestPostUser GenerateValidPostUser()
        {
            var fakerUser = new Faker<GoRestPostUser>()
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>().ToString())                
                .RuleFor(u => u.Name,(f,u) => f.Name.FullName(Enum.Parse<Gender>(u.Gender)))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Status, f => f.PickRandom<Status>().ToString());

            return fakerUser.Generate();            
        }

        public static GoRestPostUser GenerateInvalidPostUser()
        {
            var fakerUser = new Faker<GoRestPostUser>()
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>().ToString())
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName(Enum.Parse<Gender>(u.Gender)))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Status, f => Guid.NewGuid().ToString());

            return fakerUser.Generate();
        }     

        public static async Task<HttpResponseMessage> SendPostRequestAsync(this HttpClient httpClient, GoRestPostUser postUser)
        {
            var postUserAsJson = JsonConvert.SerializeObject(postUser);

            return await httpClient.SendAsync(HttpMessageHelper.CreateRequestMessage("", HttpMethod.Post, postUserAsJson));            
        }

        public static async Task<GoRestUser> CreateUser(this HttpClient httpClient)
        {
            var response = await httpClient.SendPostRequestAsync(GenerateValidPostUser());
            return JsonConvert.DeserializeObject<GoRestUser>(await response.Content.ReadAsStringAsync());
        }
    }
}
