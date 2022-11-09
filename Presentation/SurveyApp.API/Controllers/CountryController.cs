using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SurveyApp.Application.Repositories;
using SurveyApp.Application.UnitOfWork;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistence.Contexts;
using SurveyApp.Persistence.Repositories;
using SurveyApp.Persistence.UnitOfWork;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController<Country>
    {
        private readonly ICountryRepository _countryRepository;
        public CountryController(IUnitOfWork service) : base(service)
        {
            _countryRepository = service.CountryRepository;
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _countryRepository.GetByIdAsync(id);
            return Ok(data);
        }
        [HttpGet("GetBy")]
        public async Task<IActionResult> GetBy(string? id, string? code)
        {
            if (id is not null)
            {
                var data = await _countryRepository.GetByIdAsync(id);
                return Ok(data);
            }
            else if (code is not null)
            {
                var data = await _countryRepository.GetSingleAsync(x => x.Code == code);
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet("GetAll/{justActive}")]
        public async Task<IActionResult> GetAll(bool justActive)
        {
            var data = await _countryRepository.GetListAsync(default, justActive);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] Country country)
        {
            await _countryRepository.AddAsync(country);
            await _countryRepository.SaveAsync();
            return Ok();
        }
    }

}
