using BH.Case.Application.Commands;
using BH.Case.Application.Handlers;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using Moq;

namespace BH.Case.Tests.Unit
{
	public class CreateAccountCommandHandlerTests
	{
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly Mock<ICustomerRepository> _customerRepositoryMock;
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
		private readonly CreateAccountCommandHandler _handler;

		public CreateAccountCommandHandlerTests()
		{
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_customerRepositoryMock = new Mock<ICustomerRepository>();
			_transactionRepositoryMock = new Mock<ITransactionRepository>();
			_handler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _transactionRepositoryMock.Object, _customerRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_WithValidRequest_ShouldCreateAccount()
		{
			// Arrange
			var command = new CreateAccountCommand { CustomerId = 1 };
			var customer = new Customer { Id = 1, Name = "Test", Surname = "Customer" };

			_customerRepositoryMock.Setup(x => x.GetByIdAsync(command.CustomerId))
				.ReturnsAsync(customer);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(command.CustomerId, result.CustomerId);
			_accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Once);
		}

		[Fact]
		public async Task Handle_WithInvalidCustomerId_ShouldThrowKeyNotFoundException()
		{
			// Arrange
			var command = new CreateAccountCommand { CustomerId = 999 };

			_customerRepositoryMock.Setup(x => x.GetByIdAsync(command.CustomerId))
				.ReturnsAsync((Customer?)null);

			// Act & Assert
			await Assert.ThrowsAsync<KeyNotFoundException>(() => 
				_handler.Handle(command, CancellationToken.None));

			_accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Never);
		}
	}
}

