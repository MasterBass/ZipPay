using TestProject.Core.Domain;
using TestProject.Storage.DAL;
using TestProject.Storage.Repositories.Core;
using TestProject.Storage.Repositories.Interfaces;

namespace TestProject.Storage.Repositories.Implementation
{
    public class UserRepository : GenericRepository<AppDbContext, User, long>, IUserRepository
    {
        protected UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}