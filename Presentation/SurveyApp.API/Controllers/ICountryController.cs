using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Controllers
{
    public interface ICountryController
    {
        IActionResult Get();
        IActionResult OrderedByUserCount();
    }
}