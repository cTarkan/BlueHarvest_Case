using BlueHarvest_Case.Application.Commands;
using BlueHarvest_Case.Application.Handlers;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace BlueHarvest_Case.Tests.Unit
{
	public class CreateUserCommandHandlerTests
	{
		private readonly Mock<IUserRepository> _userRepositoryMock;
		private readonly CreateUserCommandHandler _handler;

		public CreateUserCommandHandlerTests()
		{
			_userRepositoryMock = new Mock<IUserRepository>();
			_handler = new CreateUserCommandHandler(_userRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_WithValidRequest_ShouldCreateUser()
		{
			// Arrange
			var command = new CreateUserCommand 
			{ 
				Name = "John",
				Surname = "Doe"
			};

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(command.Name, result.Name);
			Assert.Equal(command.Surname, result.Surname);
			_userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
		}

		[Theory]
		[InlineData("", "Doe")]
		[InlineData("John", "")]
		[InlineData(null, "Doe")]
		[InlineData("John", null)]
		public async Task Handle_WithInvalidData_ShouldThrowArgumentException(string name, string surname)
		{
			// Arrange
			var command = new CreateUserCommand
			{
				Name = name,
				Surname = surname
			};

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
				await _handler.Handle(command, CancellationToken.None));

			_userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
		}
	}
}
