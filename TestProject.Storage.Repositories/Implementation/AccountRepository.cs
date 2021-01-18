using TestProject.Core.Domain;
using TestProject.Storage.DAL;
using TestProject.Storage.Repositories.Core;
using TestProject.Storage.Repositories.Interfaces;

namespace TestProject.Storage.Repositories.Implementation
{
    public class AccountRepository : GenericRepository<AppDbContext, Account, long>, IAccountRepository
    {
        protected AccountRepository(AppDbContext context) : base(context)
        {
        }
    }
}