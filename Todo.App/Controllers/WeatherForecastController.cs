using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace Todo.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"

        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IHttpContextAccessor httpContextAccessor, ILogger<WeatherForecastController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var user = _userManager.GetUserId(User);
            //var user2 = _userManager.GetUserAsync(User).Result;
            //_userManager.f
            
            //User.Claims.FirstOrDefault()
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
