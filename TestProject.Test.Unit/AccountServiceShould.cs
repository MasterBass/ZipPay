using System;
using System.Linq.Expressions;
using Moq;
using TestProject.Common.Exception;
using TestProject.Core.Domain;
using TestProject.Core.Services;
using TestProject.Storage.DAL.Exceptions;
using TestProject.Storage.Repositories.Core;
using TestProject.Storage.Repositories.Interfaces;
using Xunit;

namespace TestProject.Test.Unit
{
    public class AccountServiceShould
    {
        private readonly Mock<IAccountRepository> _mockRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private AccountService _sut;

        public AccountServiceShould()
        {
            _mockRepository = new Mock<IAccountRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void CreateAccountErrorIfUserNotFound()
        {
            _mockUserRepository.Setup(x =>
                    x.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(() => null);

            _sut = new AccountService(_mockRepository.Object, _mockUnitOfWork.Object, _mockUserRepository.Object);
            Assert.ThrowsAsync<EntityNotFoundException<long>>(() => _sut.Create(new Account()));
            _mockRepository.Verify(x => x.Create(It.IsAny<Account>()), Times.Never);
        }

        [Theory]
        [InlineData(0, 500)]
        [InlineData(1, 10000)]
        [InlineData(1000, 1000)]
        [InlineData(1001, 1000)]
        [InlineData(1001, 2000)]
        public async void CreateAccountErrorIfMonthlySalaryNotEnough(decimal monthlySalary, decimal monthlyExpenses)
        {
            _mockUserRepository.Setup(x =>
                    x.GetById(It.IsAny<long>()))
                .ReturnsAsync(() => new User
                {
                    Email = "abc@abc.com",
                    MonthlyExpenses = monthlyExpenses,
                    MonthlySalary = monthlySalary
                });

            _sut = new AccountService(_mockRepository.Object, _mockUnitOfWork.Object, _mockUserRepository.Object);
            await Assert.ThrowsAsync<SalaryAmountIsNotEnough>(() => _sut.Create(new Account()));
            _mockRepository.Verify(x => x.Create(It.IsAny<Account>()), Times.Never);
        }

        [Theory]
        [InlineData(1001, 1)]
        [InlineData(10000, 1000)]
        public async void CreateAccountSuccessIfMonthlySalaryEnough(decimal monthlySalary, decimal monthlyExpenses)
        {
            _mockUserRepository.Setup(x =>
                    x.GetById(It.IsAny<long>()))
                .ReturnsAsync(() => new User
                {
                    Email = "abc@abc.com",
                    MonthlyExpenses = monthlyExpenses,
                    MonthlySalary = monthlySalary
                });

            _sut = new AccountService(_mockRepository.Object, _mockUnitOfWork.Object, _mockUserRepository.Object);
            await _sut.Create(new Account());
            _mockRepository.Verify(x => x.Create(It.IsAny<Account>()), Times.Once);
        }
    }
}