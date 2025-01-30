using BlueHarvest_Case.Application.Commands;
using BlueHarvest_Case.Application.Handlers;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using Moq;

namespace BlueHarvest_Case.Tests.Unit
{
	public class CreateAccountCommandHandlerTests
	{
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly Mock<IUserRepository> _userRepositoryMock;
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
		private readonly CreateAccountCommandHandler _handler;

		public CreateAccountCommandHandlerTests()
		{
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_userRepositoryMock = new Mock<IUserRepository>();
			_transactionRepositoryMock = new Mock<ITransactionRepository>();
			_handler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _transactionRepositoryMock.Object, _userRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_WithValidRequest_ShouldCreateAccount()
		{
			// Arrange
			var command = new CreateAccountCommand { CustomerId = 1 };
			var user = new User { Id = 1, Name = "Test", Surname = "User" };

			_userRepositoryMock.Setup(x => x.GetByIdAsync(command.CustomerId))
				.ReturnsAsync(user);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(command.CustomerId, result.CustomerId);
			_accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Once);
		}

		[Fact]
		public async Task Handle_WithInvalidUserId_ShouldThrowKeyNotFoundException()
		{
			// Arrange
			var command = new CreateAccountCommand { CustomerId = 999 };

			_userRepositoryMock.Setup(x => x.GetByIdAsync(command.CustomerId))
				.ReturnsAsync((User?)null);

			// Act & Assert
			await Assert.ThrowsAsync<KeyNotFoundException>(() => 
				_handler.Handle(command, CancellationToken.None));

			_accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Never);
		}
	}
}

