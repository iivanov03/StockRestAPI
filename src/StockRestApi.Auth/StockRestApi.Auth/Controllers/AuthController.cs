using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using StockRestApi.Auth.Services.Contracts;

namespace StockRestApi.Auth.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            // Your registration logic here
            // Store user data in the database, etc.

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var token = _authService.GenerateToken(request.Username);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
