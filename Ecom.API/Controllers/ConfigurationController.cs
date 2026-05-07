using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult GetConfig()
        {
            var config = new
            {
                ConnectionStrings = _configuration["ConnectionStrings:DefaultConnection"],
                LoggingLogLevel = _configuration["Logging:LogLevel:Default"]
            };

            return Ok(config);
        }
    }
}
