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
	public class UserApiTests : IClassFixture<WebApplicationFactory<API.Program>>
	{
		private readonly HttpClient _client;

		public UserApiTests(WebApplicationFactory<API.Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task GetUserAccountDetails_ShouldReturnUserInfo()
		{
			// Arrange
			var customerId = 1;

			// Act
			var response = await _client.GetAsync($"/api/user/{customerId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var result = await response.Content.ReadFromJsonAsync<JsonElement>();
			result.GetProperty("name").GetString().Should().NotBeNullOrEmpty();
			result.GetProperty("totalBalance").GetDecimal().Should().BeGreaterThanOrEqualTo(0);
		}

		[Fact]
		public async Task GetUserAccountDetails_WithInvalidCustomer_ShouldReturnNotFound()
		{
			// Arrange
			var customerId = 999; // Olmayan müşteri

			// Act
			var response = await _client.GetAsync($"/api/user/{customerId}/details");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
	}
}
