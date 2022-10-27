using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        public ConfigController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        [HttpGet]  
        public Dictionary<string,string> Get()
        {
            var key = Configuration.GetSection("FunctionAppURL").GetChildren().ToDictionary(a => a.Key, a => a.Value);
            return key;
        }
    }
}
