using System.Threading.Tasks;
using TestProject.Common.Filtering;
using TestProject.Core.Domain;

namespace TestProject.Infrastructure.Services
{
    public interface IUserService
    {
        Task<PagedEntityResult<User>> LoadAll(PageInfoModel pageInfo = null);
        Task<User> LoadByEmail(string email);
        Task<User> Load(long id);
        Task<long> Create(User entity);
    }
}