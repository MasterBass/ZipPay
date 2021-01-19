using AutoMapper;

namespace TestProject.API.Models.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Core.Domain.User, UserViewModel>();
        }
    }
}