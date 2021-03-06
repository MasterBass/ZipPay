using TestProject.Core.Domain;
using TestProject.Storage.DAL;
using TestProject.Storage.Repositories.Core;

namespace TestProject.Storage.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<AppDbContext, User, long>
    {
        
    }
}