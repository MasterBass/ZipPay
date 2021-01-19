using System;
using System.Linq.Expressions;
using Moq;
using TestProject.Common.Exception;
using TestProject.Core.Domain;
using TestProject.Core.Services;
using TestProject.Storage.Repositories.Core;
using TestProject.Storage.Repositories.Interfaces;
using Xunit;

namespace TestProject.Test.Unit
{
    public class UserServiceShould
    {
        private readonly UserService _sut;
        private readonly Mock<IUserRepository> _mockRepository;

        public UserServiceShould()
        {
            _mockRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockRepository.Setup(x =>
                    x.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(() => new User
                {
                    Email = "test@abc.com",
                    MonthlySalary = 5000,
                    MonthlyExpenses = 2500
                });

            _sut = new UserService(_mockRepository.Object, mockUnitOfWork.Object);
        }

        [Fact]
        public async void CreateUserErrorIfUserAlreadyExists()
        {
            await Assert.ThrowsAsync<UserAlreadyExists>(() => _sut.Create(new User()));
            _mockRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }
    }
}