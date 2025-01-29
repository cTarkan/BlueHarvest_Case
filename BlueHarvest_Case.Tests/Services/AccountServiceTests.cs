using BlueHarvest_Case.Application.Interfaces;
using BlueHarvest_Case.Application.Services;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;


namespace BlueHarvest_Case.Tests.Services
{
	public class AccountServiceTests
	{
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly Mock<ITransactionService> _transactionServiceMock;
		private readonly AccountService _accountService;

		public AccountServiceTests()
		{
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_transactionServiceMock = new Mock<ITransactionService>();
			_accountService = new AccountService(_accountRepositoryMock.Object, _transactionServiceMock.Object);
		}

		[Fact]
		public async Task CreateAccountAsync_WithZeroInitialCredit_ShouldCreateAccountWithoutTransaction()
		{
			// Arrange
			int customerId = 1;
			decimal initialCredit = 0;
			var account = new Account(customerId, initialCredit);
			_accountRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Account>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _accountService.CreateAccountAsync(customerId, initialCredit);

			// Assert
			result.Should().NotBeNull();
			result.Balance.Should().Be(0);
			_transactionServiceMock.Verify(ts => ts.AddTransactionAsync(It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
		}

		[Fact]
		public async Task CreateAccountAsync_WithPositiveInitialCredit_ShouldCreateTransaction()
		{
			// Arrange
			int customerId = 1;
			decimal initialCredit = 100;
			var account = new Account(customerId, 0);
			_accountRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Account>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _accountService.CreateAccountAsync(customerId, initialCredit);

			// Assert
			result.Should().NotBeNull();
			result.Balance.Should().Be(100);
			_transactionServiceMock.Verify(ts => ts.AddTransactionAsync(It.IsAny<int>(), 100), Times.Once);
		}

		[Fact]
		public async Task CreateAccountAsync_WithNegativeInitialCredit_ShouldThrowException()
		{
			// Arrange
			int customerId = 1;
			decimal initialCredit = -100;

			// Act
			var act = async () => await _accountService.CreateAccountAsync(customerId, initialCredit);

			// Assert
			await act.Should().ThrowAsync<ArgumentException>();
		}
	}
}
