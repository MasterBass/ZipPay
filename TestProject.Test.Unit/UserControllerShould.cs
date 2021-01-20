using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestProject.API.Controllers;
using TestProject.API.Models.User;
using TestProject.Core.Domain;
using TestProject.Infrastructure.Services;
using Xunit;

namespace TestProject.Test.Unit
{
    public class UserControllerShould
    {
        private readonly UserController _sut;

        public UserControllerShould()
        {
            var mockService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            mockService.Setup(x => x.Load(1))
                .ReturnsAsync(() => new User
                {
                    Email = "test@abc.com",
                    MonthlySalary = 5000,
                    MonthlyExpenses = 2500
                });
            mockMapper.Setup(x => x.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(new UserViewModel
                {
                    Id = "1",
                    Email = "test@abc.com",
                    MonthlySalary = 5000,
                    MonthlyExpenses = 2500
                });
            
            _sut = new UserController(mockService.Object, mockMapper.Object);
        }

        [Fact]
        public async void GetUserByIdNotFoundError()
        {
            var result = await _sut.Get(5);
            var notFoundObjecResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundObjecResult.StatusCode);
            System.Type type = notFoundObjecResult.Value.GetType(); 
            Assert.Equal("Entities not found", 
                type.GetProperty("message")?.GetValue(notFoundObjecResult.Value, null));
            
        }
        
        [Fact]
        public async void GetUserByIdSuccess()
        {
            var result = await _sut.Get(1);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var userViewModel = Assert.IsType<UserViewModel>(okObjectResult.Value);
            Assert.Equal("1", userViewModel.Id);
            Assert.Equal("test@abc.com", userViewModel.Email);
            Assert.Equal(5000, userViewModel.MonthlySalary);
            Assert.Equal(2500, userViewModel.MonthlyExpenses);
        }
    }
}