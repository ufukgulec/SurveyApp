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
    public class UserController : BaseController<Role>
    {

        public UserController(IUnitOfWork service) : base(service)
        {

        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await service.UserRepository.GetByIdAsync(id);
            return Ok(data);
        }
        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var data = await service.UserRepository.GetSingleAsync(x => x.Email == email);
            return Ok(data);
        }
        [HttpGet("GetAll/{justActive}")]
        public async Task<IActionResult> GetAll(bool justActive)
        {
            var data = await service.UserRepository.GetListAsync(default, justActive, x => x.Profile.Country, x => x.Profile.Role);
            return Ok(data);
        }
    }
}
