using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Linq;


namespace BH.Case.Tests.Integration
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
			var createUserRequest = new { Name = "John", Surname = "Doe" };
			var userResponse = await _client.PostAsJsonAsync("/api/user", createUserRequest);
			var user = await userResponse.Content.ReadFromJsonAsync<JsonElement>();
			var userId = user.GetProperty("id").GetInt32();

			var request = new { CustomerId = userId, InitialCredit = 100 };

			// Act
			var response = await _client.PostAsJsonAsync("/api/account", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Created);

			var result = await response.Content.ReadFromJsonAsync<JsonElement>();
			result.GetProperty("customerId").GetInt32().Should().Be(userId);
			result.GetProperty("balance").GetDecimal().Should().Be(100);
			result.GetProperty("id").GetInt32().Should().BeGreaterThan(0);
		}

		[Fact]
		public async Task GetUserAccountDetails_ShouldReturnCorrectData()
		{
			// Arrange
			// Create user first
			var createUserRequest = new { Name = "John", Surname = "Doe" };
			var userResponse = await _client.PostAsJsonAsync("/api/user", createUserRequest);
			var user = await userResponse.Content.ReadFromJsonAsync<JsonElement>();
			var userId = user.GetProperty("id").GetInt32();

			// Create account for the user
			var accountRequest = new { CustomerId = userId, InitialCredit = 100 };
			await _client.PostAsJsonAsync("/api/account", accountRequest);

			// Act
			var response = await _client.GetAsync($"/api/account/{userId}");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var accounts = await response.Content.ReadFromJsonAsync<JsonElement>();
			var firstAccount = accounts.EnumerateArray().First();
			
			firstAccount.GetProperty("customerId").GetInt32().Should().Be(userId);
			firstAccount.GetProperty("balance").GetDecimal().Should().Be(100);
			firstAccount.GetProperty("id").GetInt32().Should().BeGreaterThan(0);
		}

		[Fact]
		public async Task AddTransaction_ShouldUpdateBalance()
		{
			// Arrange
			// Create user first
			var createUserRequest = new { Name = "John", Surname = "Doe" };
			var userResponse = await _client.PostAsJsonAsync("/api/user", createUserRequest);
			var user = await userResponse.Content.ReadFromJsonAsync<JsonElement>();
			var userId = user.GetProperty("id").GetInt32();

			// Create account
			var accountRequest = new { CustomerId = userId, InitialCredit = 0 };
			var createAccountResponse = await _client.PostAsJsonAsync("/api/account", accountRequest);
			createAccountResponse.StatusCode.Should().Be(HttpStatusCode.Created);

			var createdAccount = await createAccountResponse.Content.ReadFromJsonAsync<JsonElement>();
			var accountId = createdAccount.GetProperty("id").GetInt32();

			var transactionRequest = new { AccountId = accountId, Amount = 50 };

			// Act
			var addTransactionResponse = await _client.PostAsJsonAsync("/api/transaction", transactionRequest);

			// Assert
			addTransactionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var userDetailsResponse = await _client.GetAsync($"/api/user/{userId}/details");
			userDetailsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var userDetails = await userDetailsResponse.Content.ReadFromJsonAsync<JsonElement>();
			var accountDetails = userDetails.GetProperty("accounts").EnumerateArray()
				.First(a => a.GetProperty("accountId").GetInt32() == accountId);
			accountDetails.GetProperty("balance").GetDecimal().Should().Be(50);
		}
	}
}
