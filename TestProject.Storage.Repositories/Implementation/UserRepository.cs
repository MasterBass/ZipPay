using TestProject.Common.Attribute;
using TestProject.Core.Domain;
using TestProject.Storage.DAL;
using TestProject.Storage.Repositories.Core;
using TestProject.Storage.Repositories.Interfaces;

namespace TestProject.Storage.Repositories.Implementation
{
    [ExposeForDI]
    public class UserRepository : GenericRepository<AppDbContext, User, long>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}