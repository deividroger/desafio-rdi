using desafio_rdi.webapi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace desafio_rdi_tests.Fixtures
{
    public class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public TestContext() => SetupClient();

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}
