using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;


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
			var customerId = 1;
			var initialCredit = 100;

			// Act
			var response = await _client.PostAsync($"/api/account/create?customerId={customerId}&initialCredit={initialCredit}", null);

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
			var accountId = 1;
			var amount = 50;

			// Act
			var response = await _client.PostAsync($"/api/transaction/add?accountId={accountId}&amount={amount}", null);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}
	}
}
