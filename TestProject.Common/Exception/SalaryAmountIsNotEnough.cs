using TestProject.Core.Domain;

namespace TestProject.Common.Exception
{
    public class SalaryAmountIsNotEnough : TestProjectException
    {
        public SalaryAmountIsNotEnough(User user, decimal minimalAmount) 
        {
            User = user;
            MinimalAmount = minimalAmount;
        }

        private User User { get; }

        private decimal MinimalAmount { get; }

        public override string Message => $"Current monthly balance is {User.MonthlySalary - User.MonthlyExpenses}. " +
                                          $"For creating account it must be more than {MinimalAmount}";
    }
}