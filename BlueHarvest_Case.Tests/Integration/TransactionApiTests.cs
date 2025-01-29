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
			var request = new { accountId = 1, amount = 50 };

			// Act
			var response = await _client.PostAsJsonAsync("/api/transaction/add", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task GetTransactionsByCustomerId_ShouldReturnTransactions()
		{
			// Arrange
			var customerId = 1;
			var accountRequest = new { customerId, initialCredit = 0 };
			var transactionRequest = new { accountId = 1, amount = 50 };

			var createAccountResponse = await _client.PostAsJsonAsync("/api/account/create", accountRequest);
			createAccountResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var addTransactionResponse = await _client.PostAsJsonAsync("/api/transaction/add", transactionRequest);
			addTransactionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

			var response = await _client.GetAsync($"/api/transaction/{customerId}/transactions");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var result = await response.Content.ReadFromJsonAsync<JsonElement>();
			result.GetArrayLength().Should().BeGreaterThan(0);
		}
	}
}
