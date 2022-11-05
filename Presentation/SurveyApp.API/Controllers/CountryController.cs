using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SurveyApp.Application.Repositories;
using SurveyApp.Domain.Entities;
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
        private readonly IUserRepository _userRepository;
        public CountryController(ICountryRepository countryRepository, IUserRepository userRepository) : base()
        {
            _countryRepository = countryRepository;
            _userRepository = userRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            //var data = _userRepository.GetListAsync(null).GetAwaiter().GetResult();
            var data = _userRepository.GetList(x => x.Name == "Ufuk Güleç", default, x => x.Profile.Role);
            //if (data.Count == 0)
            //{
            //    logger.Information("About page visited at {DT}",
            //DateTime.Now.ToLongTimeString());
            //}
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
