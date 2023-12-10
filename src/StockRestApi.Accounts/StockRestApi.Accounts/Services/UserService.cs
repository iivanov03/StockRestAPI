using System;
using StockRestApi.Accounts.Authorization;
using StockRestApi.Accounts.Entities;
using StockRestApi.Accounts.Models;
using StockRestApi.Accounts.Models;

namespace StockRestApi.Accounts.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(int id);
    Task Create(CreateRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
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

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    public async Task Create(CreateRequest model)
    {
        if (await _userRepository.GetByEmail(model.Email!) != null)
            throw new AppException("User with the email '" + model.Email + "' already exists");

        var user = _mapper.Map<User>(model);

        user.PasswordHash = BCrypt.HashPassword(model.Password);

        await _userRepository.Create(user);
    }

    public async Task Update(int id, UpdateRequest model)
    {
        var user = await _userRepository.GetById(id);

        if (user == null)
            throw new KeyNotFoundException("User not found");

        var emailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;
        if (emailChanged && await _userRepository.GetByEmail(model.Email!) != null)
            throw new AppException("User with the email '" + model.Email + "' already exists");

        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCrypt.HashPassword(model.Password);

        _mapper.Map(model, user);

        await _userRepository.Update(user);
    }

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }

    public async Task AddMoney(int userId, double amount, string currency)
    {
        var user = await _userRepository.GetById(userId);

        if (user == null)
            throw new KeyNotFoundException("User not found");

        if (currency != "USD")
        {
            double conversionRate = await _exchangeRateService.GetExchangeRate(currency, "USD");

            amount *= conversionRate;
        }

        user.Balance += amount;

        UpdateTier(user);

        await _userRepository.Update(user);
    }

    private void UpdateTier(User user)
    {
        if (user.Balance >= 100000)
            user.Tier = "Platinum";
        else if (user.Balance >= 10000)
            user.Tier = "Gold";
        else if (user.Balance >= 1000)
            user.Tier = "Silver";
        else
            user.Tier = "Bronze";
    }

}

