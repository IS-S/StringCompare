using SomeProject.Web.Application.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SomeProject.Web.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IOptionsSnapshot<ApplicationSettings> _options;

        public HomeController(IOptionsSnapshot<ApplicationSettings> options)
        {
            _options = options;
        }

        [HttpGet("/test")]
        public IActionResult Index()
        {
            return Ok(_options.Value);
        }
    }
}
