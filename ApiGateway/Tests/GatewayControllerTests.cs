//using Xunit;
//using Microsoft.AspNetCore.Mvc.Testing;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace ApiGateway.Tests
//{
//    public class GatewayControllerTests : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly HttpClient _client;

//        public GatewayControllerTests(WebApplicationFactory<Program> factory)
//        {
//            _client = factory.CreateClient();
//        }

//        [Fact]
//        public async Task Health_ReturnsOk()
//        {
//            var response = await _client.GetAsync("/api/gateway/health");
//            response.EnsureSuccessStatusCode();
//            var content = await response.Content.ReadAsStringAsync();
//            Assert.Equal("Healthy", content);
//        }

//        [Fact]
//        public async Task Status_ReturnsOk()
//        {
//            var response = await _client.GetAsync("/api/gateway/status");
//            response.EnsureSuccessStatusCode();
//            var content = await response.Content.ReadAsStringAsync();
//            Assert.Contains("API Gateway is running.", content);
//        }
//    }
//}