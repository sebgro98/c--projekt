using Microsoft.AspNetCore.Mvc;
using AuthApi.Models;
using AuthApi.DTOs;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        //dependency injektion utav instancen AuthService
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/signup
        [HttpPost("signup")]
        public async Task<ActionResult<User>> Signup([FromBody] User user)
        {
            var result = await _authService.RegisterUserAsync(user);
            if (result.Success)
            {
                return CreatedAtAction(nameof(Signin), new { id = result.User.Id }, result.User);
            }
            return BadRequest(result.Message);
        }


        // POST: api/auth/signin
        [HttpPost("signin")]
        public async Task<ActionResult<string>> Signin(UserSigninDto userSigninDto)
        {
            var token = await _authService.AuthenticateAsync(userSigninDto);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
