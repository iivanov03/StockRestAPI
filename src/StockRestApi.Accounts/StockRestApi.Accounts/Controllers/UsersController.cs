namespace StockRestApi.Accounts.Controllers;

using Microsoft.AspNetCore.Mvc;
using StockRestApi.Accounts.Authorization;
using StockRestApi.Accounts.Models;
using StockRestApi.Accounts.Services;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private IUserService _userService;

    public AccountsController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost()]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }
}