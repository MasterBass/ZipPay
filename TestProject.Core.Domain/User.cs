using System.ComponentModel.DataAnnotations.Schema;
using TestProject.Storage.Core;

namespace TestProject.Core.Domain
{
    public class User : IEntity<long>
    {
        public long Id { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal MonthlySalary { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal MonthlyExpenses { get; set; }
        public virtual Account Account { get; set; }
    }
}