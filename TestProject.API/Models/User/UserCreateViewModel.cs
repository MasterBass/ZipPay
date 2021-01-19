using System.ComponentModel.DataAnnotations;

namespace TestProject.API.Models.User
{
    public class UserCreateViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value for Monthly Salary bigger than {1}")]
        public decimal MonthlySalary { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value for Monthly Expenses bigger than {1}")]
        public decimal MonthlyExpenses { get; set; }
    }
}