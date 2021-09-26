using Calendar.Endpoints.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Calendar.ApplicationService.Test
{
    public class CalendarApiTest
    {
        private readonly HttpClient _client;

        public CalendarApiTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetAllEventItemsTestAsync(string method)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/v1/Calendar/");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", "")]
        public async Task AlbumGetTestAsync(string method, Guid id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/v1/Calendar/{id}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
