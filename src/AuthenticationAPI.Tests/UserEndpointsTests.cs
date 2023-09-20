using Application.Users;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace AuthenticationAPI.Tests
{
    public class UserEndpointsTests
    {
        private readonly HttpClient _client;

        public UserEndpointsTests()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            var appFactory = new WebApplicationFactory<AuthenticationAPI.Program>().
                WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });
            });

            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task ShouldCreateUser()
        {
            //Arrange
            var command = new CreateUserCommand("JohnDoe", "teste@test.com", "BeautifulLife", "User");

            //Act
            var response = await _client.PostAsJsonAsync("/users", command);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
