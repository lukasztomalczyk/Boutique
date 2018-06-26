
using Bountique.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
                .UseEnvironment("Testing")
                .UseStartup<Startup>());

            client = server.CreateClient();
            client.BaseAddress = new Uri("http://havanaApi/");
        }

        [Test]
        public async Task  Run()
        {
            var query = "api/Poc";

            var response = await client.GetAsync(query);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
