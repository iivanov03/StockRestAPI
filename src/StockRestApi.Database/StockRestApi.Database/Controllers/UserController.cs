using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

using StockRestApi.Database.Models.Users;
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
            var userId = await _userService.RegisterAsync(model.Username, model.Email, model.Password);
            if (userId is not null)
            {
                return Ok(userId);
            }

            return BadRequest("User already exists");
        }

        [HttpPost("GetUserId")]
        public async Task<IActionResult> GetUserId(LoginRequest model)
        {
            var userId = await _userService.GetUserId(model.Username, model.Password);
            if (userId is not null)
            {
                return Ok(userId);
            }

            return BadRequest("User does not exist");
        }
    }
}
