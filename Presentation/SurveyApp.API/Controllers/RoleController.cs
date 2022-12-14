using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurveyApp.Application.Repositories;
using SurveyApp.Application.UnitOfWork;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistence.Repositories;
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

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _roleRepository.GetByIdAsync(id);
            return Ok(data);
        }
        [HttpGet("GetAll/{justActive}")]
        public async Task<IActionResult> GetAll(bool justActive)
        {
            var data = await _roleRepository.GetListAsync(default, justActive);
            return Ok(data);
        }
    }

}
