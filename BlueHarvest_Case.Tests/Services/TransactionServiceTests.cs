using BlueHarvest_Case.Application.Services;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace BlueHarvest_Case.Tests.Services
{
	public class TransactionServiceTests
	{
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly TransactionService _transactionService;

		public TransactionServiceTests()
		{
			_transactionRepositoryMock = new Mock<ITransactionRepository>();
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_transactionService = new TransactionService(_transactionRepositoryMock.Object, _accountRepositoryMock.Object);
		}

		[Fact]
		public async Task AddTransactionAsync_WithValidAccount_ShouldCallRepository()
		{
			// Arrange
			int accountId = 1;
			decimal amount = 100;
			_transactionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Transaction>()))
				.Returns(Task.CompletedTask);

			// Act
			await _transactionService.AddTransactionAsync(accountId, amount);

			// Assert
			_transactionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Transaction>()), Times.Once);
		}

		[Fact]
		public async Task GetTransactionsByCustomerIdAsync_WithValidCustomer_ShouldReturnTransactions()
		{
			// Arrange
			int customerId = 1;
			var accounts = new List<Account>
			{
				new Account(customerId, 500) { Id = 1 },
				new Account(customerId, 300) { Id = 2 }
			};

			var transactions = new List<Transaction>
			{
				new Transaction(1, 100) { Id = 1 },
				new Transaction(2, 200) { Id = 2 }
			};

			_accountRepositoryMock.Setup(repo => repo.GetByCustomerIdAsync(customerId))
				.ReturnsAsync(accounts);

			_transactionRepositoryMock.Setup(repo => repo.GetByAccountIdAsync(It.IsAny<int>()))
				.ReturnsAsync((int id) => transactions.FindAll(t => t.AccountId == id));

			// Act
			var result = await _transactionService.GetTransactionsByCustomerIdAsync(customerId);

			// Assert
			result.Should().HaveCount(2);
			result.Should().Contain(t => t.Amount == 100);
			result.Should().Contain(t => t.Amount == 200);
		}
	}
}
