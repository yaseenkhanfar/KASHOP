using KASHOP.BLL.Services;
using KASHOP.DAL.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationservice;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationservice = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {
            var result = await _authenticationservice.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var result = await _authenticationservice.LoginAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmEmailRequest request)
        {
            var result = await _authenticationservice.ConfirmEmail(request);
            if (!result) return BadRequest();
            return Ok();
        }
    }
}
