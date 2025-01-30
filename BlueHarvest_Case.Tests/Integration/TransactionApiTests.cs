using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlueHarvest_Case.Tests.Integration
{
	public class TransactionApiTests : IClassFixture<WebApplicationFactory<API.Program>>
	{
		private readonly HttpClient _client;

		public TransactionApiTests(WebApplicationFactory<API.Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task AddTransaction_ShouldReturnOk()
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
			var createdAccount = await createAccountResponse.Content.ReadFromJsonAsync<JsonElement>();
			var accountId = createdAccount.GetProperty("id").GetInt32();

			var transactionRequest = new { AccountId = accountId, Amount = 50 };

			// Act
			var response = await _client.PostAsJsonAsync("/api/transaction", transactionRequest);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task GetTransactionsByCustomerId_ShouldReturnTransactions()
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

			// Add transaction
			var transactionRequest = new { AccountId = accountId, Amount = 50 };
			var addTransactionResponse = await _client.PostAsJsonAsync("/api/transaction", transactionRequest);
			addTransactionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			// Act
			var response = await _client.GetAsync($"/api/user/{userId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var userDetails = await response.Content.ReadFromJsonAsync<JsonElement>();
			var transactions = userDetails.GetProperty("accounts").EnumerateArray()
				.First(a => a.GetProperty("accountId").GetInt32() == accountId)
				.GetProperty("transactions");
			transactions.GetArrayLength().Should().BeGreaterThan(0);
		}
	}
}
