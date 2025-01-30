using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlueHarvest_Case.Tests.Integration
{
	public class UserApiTests : IClassFixture<WebApplicationFactory<API.Program>>
	{
		private readonly HttpClient _client;

		public UserApiTests(WebApplicationFactory<API.Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task CreateUser_ShouldReturnOk()
		{
			// Arrange
			var request = new { Name = "John", Surname = "Doe" };

			// Act
			var response = await _client.PostAsJsonAsync("/api/user", request);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var result = await response.Content.ReadFromJsonAsync<JsonElement>();
			result.GetProperty("name").GetString().Should().Be("John");
			result.GetProperty("surname").GetString().Should().Be("Doe");
			result.GetProperty("id").GetInt32().Should().BeGreaterThan(0);
		}

		[Fact]
		public async Task GetUserById_ShouldReturnUser()
		{
			// Arrange
			var createRequest = new { Name = "John", Surname = "Doe" };
			var createResponse = await _client.PostAsJsonAsync("/api/user", createRequest);
			var createdUser = await createResponse.Content.ReadFromJsonAsync<JsonElement>();
			var userId = createdUser.GetProperty("id").GetInt32();

			// Act
			var response = await _client.GetAsync($"/api/user/{userId}");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var user = await response.Content.ReadFromJsonAsync<JsonElement>();
			user.GetProperty("id").GetInt32().Should().Be(userId);
			user.GetProperty("name").GetString().Should().Be("John");
			user.GetProperty("surname").GetString().Should().Be("Doe");
		}

		[Fact]
		public async Task GetUserAccountDetails_ShouldReturnUserWithAccounts()
		{
			// Arrange
			// Create user
			var createUserRequest = new { Name = "John", Surname = "Doe" };
			var userResponse = await _client.PostAsJsonAsync("/api/user", createUserRequest);
			var user = await userResponse.Content.ReadFromJsonAsync<JsonElement>();
			var userId = user.GetProperty("id").GetInt32();

			// Create account with initial credit
			var accountRequest = new { CustomerId = userId, InitialCredit = 100 };
			await _client.PostAsJsonAsync("/api/account", accountRequest);

			// Act
			var response = await _client.GetAsync($"/api/user/{userId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var userDetails = await response.Content.ReadFromJsonAsync<JsonElement>();
			userDetails.GetProperty("name").GetString().Should().Be("John");
			userDetails.GetProperty("surname").GetString().Should().Be("Doe");
			
			var accounts = userDetails.GetProperty("accounts");
			accounts.GetArrayLength().Should().BeGreaterThan(0);
			
			var firstAccount = accounts.EnumerateArray().First();
			firstAccount.GetProperty("balance").GetDecimal().Should().Be(100);
		}

		[Fact]
		public async Task GetUserAccountDetails_WithNonExistentUser_ShouldReturnNotFound()
		{
			// Arrange
			var nonExistentUserId = 99999;

			// Act
			var response = await _client.GetAsync($"/api/user/{nonExistentUserId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
	}
}
