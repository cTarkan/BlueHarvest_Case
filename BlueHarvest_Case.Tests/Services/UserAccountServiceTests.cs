using BlueHarvest_Case.Application.Services;
using BlueHarvest_Case.Domain.Entities;
using BlueHarvest_Case.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHarvest_Case.Tests.Services
{
	public class UserAccountServiceTests
	{
		private readonly Mock<IAccountRepository> _accountRepositoryMock;
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
		private readonly UserAccountService _userAccountService;

		public UserAccountServiceTests()
		{
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_transactionRepositoryMock = new Mock<ITransactionRepository>();
			_userAccountService = new UserAccountService(_accountRepositoryMock.Object, _transactionRepositoryMock.Object);
		}

		[Fact]
		public async Task GetUserAccountDetailsAsync_WithValidCustomer_ShouldReturnUserData()
		{
			// Arrange
			int customerId = 1;
			decimal initialCredit = 500;

			var accounts = new List<Account>
			{
				new Account(customerId, 0) { Id = 1 } 
            };

			var transactions = new List<Transaction>
			{
				new Transaction(1, initialCredit) { Id = 1 }, 
                new Transaction(1, 100) { Id = 2 },
				new Transaction(1, 50) { Id = 3 }
			};

			_accountRepositoryMock.Setup(repo => repo.GetByCustomerIdAsync(customerId))
				.ReturnsAsync(accounts);

			_transactionRepositoryMock.Setup(repo => repo.GetByAccountIdAsync(It.IsAny<int>()))
				.ReturnsAsync((int id) => transactions.FindAll(t => t.AccountId == id));

			// Act
			var result = await _userAccountService.GetUserAccountDetailsAsync(customerId);

			// Assert
			result.Should().NotBeNull();
			result.TotalBalance.Should().Be(650); // 500 (Initial Credit Transaction) + 100 + 50
			result.Accounts.Should().HaveCount(1);
			result.Accounts[0].Transactions.Should().HaveCount(3); // Initial transaction + 2 additional transactions
			result.Accounts[0].Balance.Should().Be(650);
		}

		[Fact]
		public async Task GetUserAccountDetailsAsync_WithInvalidCustomer_ShouldReturnNull()
		{
			// Arrange
			int customerId = 9999999; 

			_accountRepositoryMock.Setup(repo => repo.GetByCustomerIdAsync(customerId))
				.ReturnsAsync(new List<Account>());

			// Act
			var result = await _userAccountService.GetUserAccountDetailsAsync(customerId);

			// Assert
			result.Should().BeNull();
		}
	}
}
