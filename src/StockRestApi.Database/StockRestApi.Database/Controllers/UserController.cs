using Microsoft.AspNetCore.Mvc;

using StockRestApi.Database.Services.Contracts;

namespace StockRestApi.Database.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var succeeded = await _userService.RegisterAsync(model.Username, model.Email, model.Password);
            if (!succeeded)
            {
                return BadRequest("User exists");
            }

            return Ok("Registration successful");
        }


        public class RegisterRequest
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
