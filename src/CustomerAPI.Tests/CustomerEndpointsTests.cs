using CustomerAPI.ViewModels;
using Domain.Customers.Create;
using Domain.Customers.Read;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace CustomerAPI.Tests
{
    public class CustomerEndpointsTests
    {
        private readonly HttpClient _client;

        public CustomerEndpointsTests()
        {
            var appFactory = new WebApplicationFactory<Program>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task ShouldCreateCustomer()
        {
            //Arrange
            var command = new CreateCustomerCommand("John Doe", "Street 1", "teste@123.com");

            //Act
            var response = await _client.PostAsJsonAsync("/customers", command);
                 
            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ShouldGetCustomer()
        {
            //Arrange
            var query = new GetCustomerQuery(Guid.NewGuid());

            //Act
            var response = await _client.GetAsync($"/customers/{query.Id}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ShouldUpdateCustomer()
        {
            //Arrange
            var command = new UpdateCustomerRequest("John Doe", "Street 1", "teste@123.com");

            //Act
            var response = await _client.PutAsJsonAsync($"/customers/{Guid.NewGuid()}", command);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ShouldDeleteCustomer()
        {
            //Act
            var response = await _client.DeleteAsync($"/customers/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
