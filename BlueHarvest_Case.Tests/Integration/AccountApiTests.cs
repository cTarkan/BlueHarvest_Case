using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using BlueHarvest_Case.Application.Services;


namespace BlueHarvest_Case.Tests.Integration
{
	public class AccountApiTests : IClassFixture<WebApplicationFactory<API.Program>>
	{
		private readonly HttpClient _client;

		public AccountApiTests(WebApplicationFactory<API.Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task CreateAccount_WithInitialCredit_ShouldReturnAccountWithTransaction()
		{
			// Arrange
			var request = new { customerId = 1, initialCredit = 100 };

			// Act
			var response = await _client.PostAsJsonAsync("/api/account/create", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var result = await response.Content.ReadFromJsonAsync<JsonElement>();
			result.GetProperty("balance").GetDecimal().Should().Be(100);
		}

		[Fact]
		public async Task GetUserAccountDetails_ShouldReturnCorrectData()
		{
			// Arrange
			var customerId = 1;

			// Act
			var response = await _client.GetAsync($"/api/user/{customerId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var result = await response.Content.ReadFromJsonAsync<JsonElement>();
			result.GetProperty("name").GetString().Should().NotBeNullOrEmpty();

		}

		[Fact]
		public async Task AddTransaction_ShouldUpdateBalance()
		{
			// Arrange 
			var uniqueCustomerId = Math.Abs(Guid.NewGuid().GetHashCode() % 100000);
			Console.WriteLine($"Generated customerId: {uniqueCustomerId}");

			UserMockService.AddUser(uniqueCustomerId, "Test", "User");

			var accountRequest = new { customerId = uniqueCustomerId, initialCredit = 0 };

			var createAccountResponse = await _client.PostAsJsonAsync("/api/account/create", accountRequest);
			createAccountResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var createdAccount = await createAccountResponse.Content.ReadFromJsonAsync<JsonElement>();
			var accountId = createdAccount.GetProperty("id").GetInt32();

			var transactionRequest = new { accountId, amount = 50 };

			// Act 
			var addTransactionResponse = await _client.PostAsJsonAsync("/api/transaction/add", transactionRequest);
			addTransactionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			// Assert 
			var userDetailsResponse = await _client.GetAsync($"/api/user/{uniqueCustomerId}/details");
			userDetailsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var result = await userDetailsResponse.Content.ReadFromJsonAsync<JsonElement>();
			result.GetProperty("totalBalance").GetDecimal().Should().Be(50);

		}
	}
}
