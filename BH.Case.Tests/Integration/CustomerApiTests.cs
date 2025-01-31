using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BH.Case.Tests.Integration
{
	public class CustomerApiTests : IClassFixture<WebApplicationFactory<API.Program>>
	{
		private readonly HttpClient _client;

		public CustomerApiTests(WebApplicationFactory<API.Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task CreateCustomer_ShouldReturnOk()
		{
			// Arrange
			var request = new { Name = "John", Surname = "Doe" };

			// Act
			var response = await _client.PostAsJsonAsync("/api/customer", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var result = await response.Content.ReadFromJsonAsync<JsonElement>();
			result.GetProperty("name").GetString().Should().Be("John");
			result.GetProperty("surname").GetString().Should().Be("Doe");
			result.GetProperty("id").GetInt32().Should().BeGreaterThan(0);
		}

		[Fact]
		public async Task GetCustomerById_ShouldReturnCustomer()
		{
			// Arrange
			var createRequest = new { Name = "John", Surname = "Doe" };
			var createResponse = await _client.PostAsJsonAsync("/api/customer", createRequest);
			var createdCustomer = await createResponse.Content.ReadFromJsonAsync<JsonElement>();
			var customerId = createdCustomer.GetProperty("id").GetInt32();

			// Act
			var response = await _client.GetAsync($"/api/customer/{customerId}");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var customer = await response.Content.ReadFromJsonAsync<JsonElement>();
			customer.GetProperty("id").GetInt32().Should().Be(customerId);
			customer.GetProperty("name").GetString().Should().Be("John");
			customer.GetProperty("surname").GetString().Should().Be("Doe");
		}

		[Fact]
		public async Task GetCustomerAccountDetails_ShouldReturnCustomerWithAccounts()
		{
			// Arrange
			// Create customer
			var createCustomerRequest = new { Name = "John", Surname = "Doe" };
			var customerResponse = await _client.PostAsJsonAsync("/api/customer", createCustomerRequest);
			var customer = await customerResponse.Content.ReadFromJsonAsync<JsonElement>();
			var customerId = customer.GetProperty("id").GetInt32();

			// Create account with initial credit
			var accountRequest = new { CustomerId = customerId, InitialCredit = 100 };
			await _client.PostAsJsonAsync("/api/account", accountRequest);

			// Act
			var response = await _client.GetAsync($"/api/customer/{customerId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var customerDetails = await response.Content.ReadFromJsonAsync<JsonElement>();
			customerDetails.GetProperty("name").GetString().Should().Be("John");
			customerDetails.GetProperty("surname").GetString().Should().Be("Doe");
			
			var accounts = customerDetails.GetProperty("accounts");
			accounts.GetArrayLength().Should().BeGreaterThan(0);
			
			var firstAccount = accounts.EnumerateArray().First();
			firstAccount.GetProperty("balance").GetDecimal().Should().Be(100);
		}

		[Fact]
		public async Task GetCustomerAccountDetails_WithNonExistentCustomer_ShouldReturnNotFound()
		{
			// Arrange
			var nonExistentCustomerId = 99999;

			// Act
			var response = await _client.GetAsync($"/api/customer/{nonExistentCustomerId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
	}
}
