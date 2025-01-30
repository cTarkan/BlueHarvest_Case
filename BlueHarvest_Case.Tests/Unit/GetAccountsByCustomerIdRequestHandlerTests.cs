using BlueHarvest_Case.Application.Handlers;
using BlueHarvest_Case.Application.Requests;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace BlueHarvest_Case.Tests.Unit
{
	public class GetAccountsByCustomerIdRequestHandlerTests
	{
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly GetAccountsByCustomerIdRequestHandler _handler;

		public GetAccountsByCustomerIdRequestHandlerTests()
		{
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_handler = new GetAccountsByCustomerIdRequestHandler(_accountRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_WithValidCustomerId_ShouldReturnAccounts()
		{
			// Arrange
			var customerId = 1;
			var request = new GetAccountsByCustomerIdRequest { CustomerId = customerId };
			var expectedAccounts = new List<Account>
			{
				new Account(customerId, 100),
				new Account(customerId, 200)
			};

			_accountRepositoryMock.Setup(x => x.GetByCustomerIdAsync(customerId))
				.ReturnsAsync(expectedAccounts);

			// Act
			var result = await _handler.Handle(request, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedAccounts.Count, result.Count());
			Assert.Equal(expectedAccounts, result);
			_accountRepositoryMock.Verify(x => x.GetByCustomerIdAsync(customerId), Times.Once);
		}

		[Fact]
		public async Task Handle_WithNonExistentCustomerId_ShouldReturnEmptyList()
		{
			// Arrange
			var customerId = 999;
			var request = new GetAccountsByCustomerIdRequest { CustomerId = customerId };

			_accountRepositoryMock.Setup(x => x.GetByCustomerIdAsync(customerId))
				.ReturnsAsync(new List<Account>());

			// Act
			var result = await _handler.Handle(request, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Empty(result);
			_accountRepositoryMock.Verify(x => x.GetByCustomerIdAsync(customerId), Times.Once);
		}
	}
}
