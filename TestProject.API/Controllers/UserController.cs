using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestProject.API.Filters;
using TestProject.API.Models.User;
using TestProject.Common.Filtering;
using TestProject.Core.Domain;
using TestProject.Infrastructure.Services;

namespace TestProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    public class UserController : CoreController
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageInfoModel pageInfo)
        {
            var users = await _service.LoadAll(pageInfo);
            return users != null ? Ok(_mapper.Map<UserResultViewModel>(users)) : EntitiesNotFound();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var user = await _service.Load(id);

            return user != null
                ? Ok(_mapper.Map<UserViewModel>(user))
                : EntitiesNotFound();
           
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateViewModel model)
        {
            var createdItemId = await _service.Create(_mapper.Map<User>(model));
            var createdItem = await _service.Load(createdItemId);
            return Created(createdItemId.ToString(), _mapper.Map<UserViewModel>(createdItem));
        }
        
    }
}