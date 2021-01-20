using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestProject.API.Filters;
using TestProject.API.Models.Account;
using TestProject.Common.Filtering;
using TestProject.Core.Domain;
using TestProject.Infrastructure.Services;

namespace TestProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    public class AccountController : CoreController
    {
        private readonly IAccountService _service;
        private readonly IMapper _mapper;

        public AccountController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageInfoModel pageInfo)
        {
            var accounts = await _service.LoadAll(pageInfo);
            return accounts != null ? Ok(_mapper.Map<AccountResultViewModel>(accounts)) : EntitiesNotFound();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountCreateViewModel model)
        {
            var createdItemId = await _service.Create(_mapper.Map<Account>(model));
            var createdItem = await _service.Load(createdItemId);
            return Created(createdItemId.ToString(), _mapper.Map<AccountViewModel>(createdItem));
        }
    }
}