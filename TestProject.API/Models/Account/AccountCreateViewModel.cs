using System.ComponentModel.DataAnnotations;

namespace TestProject.API.Models.Account
{
    public class AccountCreateViewModel
    {
        [Required]
        [MinLength(16)]
        [MaxLength(34)]
        public string Iban { get; set; }
        [Required]
        public int? UserId { get; set; }
    }
}