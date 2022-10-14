using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(UserForAuthenticationDto userForAuth)
        {
            var token = await _service.AuthencationService.CreateTokenAsync(userForAuth);
            return Ok(token);
        }
    }
}
