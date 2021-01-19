using AutoMapper;
using TestProject.Common.Filtering;

namespace TestProject.API.Models.Account
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Core.Domain.Account, AccountViewModel>();
            CreateMap<PagedEntityResult<Core.Domain.Account>, AccountResultViewModel>();
            CreateMap<AccountCreateViewModel, Core.Domain.Account>();
        }
    }
}