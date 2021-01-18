using System.Threading.Tasks;
using TestProject.Common.Filtering;
using TestProject.Core.Domain;

namespace TestProject.Infrastructure.Services
{
    public interface IAccountService
    {
        Task<PagedEntityResult<Account>> LoadAll(PageInfoModel pageInfo = null);
        Task<Account> Load(long id);
        Task<long> Create(Account entity);
    }
}