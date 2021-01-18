using System.Threading.Tasks;
using TestProject.Common.Exception;
using TestProject.Common.Filtering;
using TestProject.Core.Domain;
using TestProject.Infrastructure.Services;
using TestProject.Storage.Repositories.Core;
using TestProject.Storage.Repositories.Interfaces;

namespace TestProject.Core.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion
        
        
        #region Ctor
        public UserService(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        
        
        #region Implementation

        public async Task<PagedEntityResult<User>> LoadAll(PageInfoModel pageInfo = null)
        {
            var items = await _repository.PagedQuery(null, null, pageInfo, a => a.Account);
            var result = new PagedEntityResult<User>
            {
                Result = items.Result,
                TotalCount = items.TotalCount,
                PagingInfo = items.PagingInfo
            };
            return result;
        }

        public async Task<User> LoadByEmail(string email)
        {
            return await _repository.Find(x => x.Email == email);
        }

        public async Task<User> Load(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<long> Create(User entity)
        {
            var user = await LoadByEmail(entity.Email);
            if(user != null) throw new UserAlreadyExists(entity.Email);
            await _repository.Create(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
        
        #endregion
        
    }
}