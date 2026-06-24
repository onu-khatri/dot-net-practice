using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinimalApi.Filters;
using MinimalApi.Services;

namespace MinimalApi.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [TimerActionFilter]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUserService _userService;

        public WeatherForecastController(IUserService userService)
        {
            _userService = userService;
        }

        private static string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        //WeatherForecast/GetWeatherForecast
        [HttpGet(Name = "GetWeatherForecast")]
        [ServiceFilter(typeof(SessionDbValidationAsyncAttribute))]
        // use ServiceFilter if filter has dependencies that need to be injected, and added in Program.cs as scoped or transient, and use TypeFilter if filter has no dependencies or only has dependencies that can be resolved from the service container, and added in Program.cs as singleton.
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
        [TypeFilter(typeof(SessionDbValidationAsyncAttribute), IsReusable = true)]
        // don't need to register SessionDbValidationAsyncAttribute in Program.cs when using TypeFilter
        // If filter are singleton safe, then use TypeFilter, if filter nature required scoped or transient, then use ServiceFilter.
        // don't use IsReusable = true if filter has dependencies that are not singleton, otherwise it will cause issues with the dependencies.
        public IResult PostData(string item)
        {
            if(item == null)
            {
                return Results.BadRequest(new { Message = "Item cannot be null" });
            }

            Summaries = Summaries.Append(item).ToArray();
            var serviceResult = _userService.GetAll();

            return Results.Ok(new { Message = "Post request received" });
        }

        //WeatherForecast/crack/123
        [HttpDelete("{item}/{index = 1}")]
        public IResult Delete(string item, int index, [FromQuery] string userName, [FromServices] IUserService userService)
        {
            var users = userService.GetAll();
            Summaries = Summaries.Where(s => s != item).ToArray();
            return Results.Ok(new { Message = $"Delete request received for ID: {item}" });
        }
    }
}
