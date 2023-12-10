using Microsoft.AspNetCore.Mvc;

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
            var succeeded = await _userService.RegisterAsync(model.Username, model.Email, model.Password);
            if (succeeded)
            {
                return Ok("Registration successful");
            }

            return BadRequest("User already exists");
        }

        public async Task<IActionResult> DoesUserExist(LoginRequest model)
        {
            var doesEsist = await _userService.DoesUserExistAsync(model.Username, model.Password);
            if (doesEsist)
            {
                return Ok();
            }

            return BadRequest("User does not exist");
        }
    }
}
