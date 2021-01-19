using System.Collections.Generic;

namespace TestProject.API.Models.User
{
    public class UserResultViewModel : ResultViewModel
    {
        public IEnumerable<UserViewModel> Result { get; set; }
    }
}