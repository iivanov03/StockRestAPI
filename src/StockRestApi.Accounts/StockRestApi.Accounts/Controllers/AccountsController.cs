namespace StockRestApi.Accounts.Controllers;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using StockRestApi.Accounts.Authorization;
using StockRestApi.Accounts.Models;
using StockRestApi.Accounts.Services;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private IUserService _userService;

    public AccountsController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterModel model)
    {
        var response = await _userService.Register(model);

        if (response == null)
            return BadRequest();

        return Ok(response);
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    //[HttpGet("{id}")]
    //public IActionResult GetById(int id)
    //{
    //    var user = _userService.GetById(id);
    //    return Ok(user);
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> Update(int id, UpdateRequest model)
    //{
    //    await _userService.Update(id, model);
    //    return Ok(new { message = "User updated" });
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    await _userService.Delete(id);
    //    return Ok(new { message = "User deleted" });
    //}

}