using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurveyApp.Application.Repositories;
using SurveyApp.Application.UnitOfWork;
using SurveyApp.Domain.Entities;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<Role>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IUnitOfWork service) : base(service)
        {
            _roleRepository = service.RoleRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            //var data = _userRepository.GetListAsync(x => x.Name == "Admin", x => x.Profile.Role).GetAwaiter().GetResult();
            //var data = _userRepository.GetList(null, default, x => x.Profile.Role);
            return Ok();
        }
        [HttpGet("OrderedByUserCount")]
        public IActionResult OrderedByUserCount()
        {
            //var data = _countryRepository.GetAll();
            return Ok();
        }
    }

}
