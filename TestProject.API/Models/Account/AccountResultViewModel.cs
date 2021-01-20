using System.Collections.Generic;

namespace TestProject.API.Models.Account
{
    public class AccountResultViewModel : ResultViewModel
    {
        public IEnumerable<AccountViewModel> Result { get; set; }
    }
}