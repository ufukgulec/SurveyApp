using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Repositories;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var data = _countryRepository.GetAll();
            return Ok(data);
        }
        [HttpGet("OrderedByUserCount")]
        public IActionResult OrderedByUserCount()
        {
            var data = _countryRepository.OrderedByUserCount();
            return Ok(data);
        }
    }

}
