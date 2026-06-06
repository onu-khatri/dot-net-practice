using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinimalApi.Services;

namespace MinimalApi.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly UserService _userService;

        public WeatherForecastController(UserService userService)
        {
            _userService = userService;
        }

        private static string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        //WeatherForecast/GetWeatherForecast
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //WeatherForecast/PostWeatherForecast
        [HttpPost(Name = "PostWeatherForecast")]
        public IResult PostData(string item)
        {
            Summaries = Summaries.Append(item).ToArray();
            var serviceResult = _userService.GetAll();

            return Results.Ok(new { Message = "Post request received" });
        }

        //WeatherForecast
        [HttpDelete("{item}/{index = 1}")]
        public IResult Delete(string item, int index, [FromServices] UserService userService)
        {
            var users = userService.GetAll();
            Summaries = Summaries.Where(s => s != item).ToArray();
            return Results.Ok(new { Message = $"Delete request received for ID: {item}" });
        }
    }
}
