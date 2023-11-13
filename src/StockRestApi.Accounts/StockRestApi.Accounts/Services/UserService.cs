using System;
using StockRestApi.Accounts.Authorization;
using StockRestApi.Accounts.Entities;
using StockRestApi.Accounts.Models;
using StockRestApi.Accounts.Models;

namespace StockRestApi.Accounts.Services;

public interface IUserService
{
    AuthenticateResponse? Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User? GetById(int id);
}

public class UserService : IUserService
{
    private List<User> _users = new List<User>
    {
        new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", Balance = 1500.0, Currency = "USD" }
    };

    private readonly IJwtUtils _jwtUtils;

    public UserService(IJwtUtils jwtUtils)
    {
        _jwtUtils = jwtUtils;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        if (user == null) return null;

        var token = _jwtUtils.GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    //public async Task<User> GetById(int id)
    //{
    //    var user = await _userRepository.GetById(id);

    //    if (user == null)
    //        throw new KeyNotFoundException("User not found");

    //    return user;
    //}

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }
}

