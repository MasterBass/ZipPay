using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestProject.API.Controllers;
using TestProject.API.Models.Account;
using TestProject.Common.Filtering;
using TestProject.Core.Domain;
using TestProject.Infrastructure.Services;
using Xunit;

namespace TestProject.Test.Unit
{
    public class AccountControllerShould
    {
        private readonly Mock<IAccountService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private AccountController _sut;

        public AccountControllerShould()
        {
            _mockService = new Mock<IAccountService>();
            _mockMapper = new Mock<IMapper>();

        }
        
        [Fact]
        public async void GetAccountsNotFoundError()
        {
            _mockService.Setup(x => x.LoadAll(null))
                .ReturnsAsync(() => null);
            
            _sut = new AccountController(_mockService.Object, _mockMapper.Object);
            
            var result = await _sut.Get(null);
            var notFoundObjecResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundObjecResult.StatusCode);
            System.Type type = notFoundObjecResult.Value.GetType(); 
            Assert.Equal("Entities not found", 
                type.GetProperty("message")?.GetValue(notFoundObjecResult.Value, null));
        }
        
        [Fact]
        public async void GetAccountsSuccess()
        {
            _mockService.Setup(x => x.LoadAll(null))
                .ReturnsAsync(() => new PagedEntityResult<Account>
                {
                    Result = new[]
                    {
                        new Account
                        {
                            Iban = "902309832092430923434534",
                            Id = 1
                        }
                    }
                });
            _mockMapper.Setup(x => x.Map<AccountResultViewModel>(It.IsAny<PagedEntityResult<Account>>()))
                .Returns(new AccountResultViewModel
                {
                    Result = new []
                    {
                        new AccountViewModel
                        {
                            Id = "1",
                            Iban = "902309832092430923434534"
                        }
                    }
                    
                });
            
            _sut = new AccountController(_mockService.Object, _mockMapper.Object);
            var result = await _sut.Get(null);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var accountResultViewModel = Assert.IsType<AccountResultViewModel>(okObjectResult.Value);
            var accounts = Assert.IsType<AccountViewModel[]>(accountResultViewModel.Result);
            var account = Assert.IsType<AccountViewModel>(accounts[0]);
            Assert.Equal("1", account.Id);
            Assert.Equal("902309832092430923434534", account.Iban);
        }
    }
}