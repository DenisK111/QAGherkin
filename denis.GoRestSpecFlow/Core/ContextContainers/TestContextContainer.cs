using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using denis.GoRestSpecFlow.Models;

namespace denis.GoRestSpecFlow.Core.ContextContainers
{
    public class TestContextContainer
    {
        public HttpClient HttpClient { get; set; } = null!;

        public GoRestUser CreatedUser { get; set; } = null!;
        public GoRestPostUser ToBeCreatedUser { get; set; } = null!;

        public HttpResponseMessage ResponseMessage { get; set; } = null!;

        public bool IsValidUser { get; set; }
    }
}
