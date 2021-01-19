using AutoMapper;

namespace TestProject.API.Models.Account
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Core.Domain.Account, AccountViewModel>();
        }
    }
}