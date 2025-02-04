using BH.Case.Application.Commands;
using BH.Case.Application.Handlers;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace BH.Case.Tests.Unit
{
	public class AddTransactionCommandHandlerTests
	{
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly AddTransactionCommandHandler _handler;

		public AddTransactionCommandHandlerTests()
		{
			_transactionRepositoryMock = new Mock<ITransactionRepository>();
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_handler = new AddTransactionCommandHandler(_transactionRepositoryMock.Object, _accountRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_WithValidRequest_ShouldCreateTransaction()
		{
			// Arrange
			var command = new AddTransactionCommand { AccountId = 1, Amount = 100 };
			var account = new Account(1, 0);

			_accountRepositoryMock.Setup(x => x.GetByIdAsync(command.AccountId))
				.ReturnsAsync(account);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(command.AccountId, result.AccountId);
			Assert.Equal(command.Amount, result.Amount);
			_transactionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Transaction>()), Times.Once);
		}

		[Fact]
		public async Task Handle_WithInvalidAccountId_ShouldThrowKeyNotFoundException()
		{
			// Arrange
			var command = new AddTransactionCommand { AccountId = 999, Amount = 100 };

			_accountRepositoryMock.Setup(x => x.GetByCustomerIdAsync(command.AccountId))
				.ReturnsAsync(new List<Account>());

			// Act & Assert
			await Assert.ThrowsAsync<KeyNotFoundException>(async () => 
				await _handler.Handle(command, CancellationToken.None));

			_transactionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Transaction>()), Times.Never);
		}
	}
}
