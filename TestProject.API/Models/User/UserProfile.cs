using AutoMapper;
using TestProject.Common.Filtering;

namespace TestProject.API.Models.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Core.Domain.User, UserViewModel>();
            CreateMap<PagedEntityResult<Core.Domain.User>, UserResultViewModel>();
            CreateMap<UserCreateViewModel, Core.Domain.User>();
        }
    }
}