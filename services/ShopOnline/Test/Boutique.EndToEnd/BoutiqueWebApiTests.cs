
using Bountique.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Boutique.EndToEnd
{
    [TestFixture]
    public class BoutiqueWebApiTests
    {
        private readonly HttpClient client;
        private readonly TestServer server;
        
        public BoutiqueWebApiTests()
        {
            server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            client = server.CreateClient();
            client.BaseAddress = new Uri("http://BontiqueApi/");
        }

        [Test]
        public async Task LoginUser_ExpectJsonToken()
        {
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("applicationo/json"));
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/Users/Login");
            request.Content = new StringContent("{" +
                                                "\"UserName\" : \"TestUser\"," +
                                                "\"Password\" : \"password\"" +
                                                "}", Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async Task Run()
        {
            var response = await client.GetAsync("");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
