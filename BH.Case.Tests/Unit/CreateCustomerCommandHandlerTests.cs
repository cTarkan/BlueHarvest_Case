using BH.Case.Application.Commands;
using BH.Case.Application.Handlers;
using BH.Case.Domain.Entities;
using BH.Case.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace BH.Case.Tests.Unit
{
	public class CreateCustomerCommandHandlerTests
	{
		private readonly Mock<ICustomerRepository> _customerRepositoryMock;
		private readonly CreateCustomerCommandHandler _handler;

		public CreateCustomerCommandHandlerTests()
		{
			_customerRepositoryMock = new Mock<ICustomerRepository>();
			_handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_WithValidRequest_ShouldCreateCustomer()
		{
			// Arrange
			var command = new CreateCustomerCommand 
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
			_customerRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Once);
		}

		[Theory]
		[InlineData("", "Doe")]
		[InlineData("John", "")]
		[InlineData(null, "Doe")]
		[InlineData("John", null)]
		public async Task Handle_WithInvalidData_ShouldThrowArgumentException(string name, string surname)
		{
			// Arrange
			var command = new CreateCustomerCommand
			{
				Name = name,
				Surname = surname
			};

			// Act & Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
				await _handler.Handle(command, CancellationToken.None));

			_customerRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Never);
		}
	}
}
