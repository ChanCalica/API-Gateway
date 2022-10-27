using API_Gateway.Models.Response;
using API_Gateway.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyVaultController : ControllerBase
    {
        private readonly IKeyVaultManager _secretManager;

        public KeyVaultController(IKeyVaultManager secretManager)

        {

            _secretManager = secretManager;

        }

        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] string username = "HQWSUsername", string password = "HQWSPassword")

        {

            try

            {

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))

                {

                    return BadRequest();

                }

                string usernameValue = await _secretManager.GetSecret(username);
                string passwordValue = await _secretManager.GetSecret(password);

                if (!string.IsNullOrEmpty(usernameValue) || !string.IsNullOrEmpty(passwordValue))

                {

                    
                    var response = new KeyValue();
                  
                    response.usernameValue = usernameValue;
                    response.passwordValue = passwordValue;
                    return Ok(response);

                }

                else

                {

                    return NotFound("Secret key not found.");

                }

            }

            catch

            {

                return BadRequest("Error: Unable to read secret");

            }
        }

    }
}
