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
        [HttpGet("Get")]
        public IActionResult Get()
        {
            var data = _countryRepository.GetList();

            return Ok(data);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var data = _countryRepository.GetList();

            return Ok(data);
        }
        [HttpGet("OrderedByUserCount")]
        public IActionResult OrderedByUserCount()
        {
            //var data = _countryRepository.GetAll();
            return Ok();
        }
    }

}
