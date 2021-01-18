using System.Threading.Tasks;
using TestProject.Common.Exception;
using TestProject.Common.Filtering;
using TestProject.Core.Domain;
using TestProject.Infrastructure.Services;
using TestProject.Storage.DAL.Exceptions;
using TestProject.Storage.Repositories.Core;
using TestProject.Storage.Repositories.Interfaces;

namespace TestProject.Core.Services
{
    public class AccountService : IAccountService
    {
        #region Fields

        private readonly IAccountRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private const decimal MinimalCreditAmount = 1000;

        #endregion
        
        #region Ctor
        public AccountService(IAccountRepository repository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        #endregion
        
        
        #region Implementation
        
        public async Task<PagedEntityResult<Account>> LoadAll(PageInfoModel pageInfo = null)
        {
            
            var items = await _repository.PagedQuery(null, null, pageInfo);
            var result = new PagedEntityResult<Account>
            {
                Result = items.Result,
                TotalCount = items.TotalCount,
                PagingInfo = items.PagingInfo
            };
            return result;
        }

        public async Task<Account> Load(long id)
        {
            var item = await _repository.GetById(id);
            return item;
        }

        public async Task<long> Create(Account entity)
        {
            var user = await _userRepository.GetById(entity.UserId);
            if (user == null)
            {
                throw new EntityNotFoundException<long>(typeof(User).Name, entity.UserId);
            }

            if (user.MonthlySalary - user.MonthlyExpenses < MinimalCreditAmount)
            {
                throw new SalaryAmountIsNotEnough(user, MinimalCreditAmount);
            }
            
            await _repository.Create(entity);
            await _unitOfWork.SaveChangesAsync();

            return entity.Id;
        }
        #endregion
    }
}