using System;
using System.Text;

using Newtonsoft.Json;

using StockRestApi.Accounts.Authorization;
using StockRestApi.Accounts.Entities;
using StockRestApi.Accounts.Helpers;
using StockRestApi.Accounts.Models;
using StockRestApi.Accounts.Models;

namespace StockRestApi.Accounts.Services;

public interface IUserService
{
    Task<string> Authenticate(AuthenticateRequest model);
    Task<bool> Register(RegisterModel model);
    Task<IEnumerable<User>> GetAll();
    User? GetById(int id);
    //Task Create(CreateRequest model);
    //Task Update(int id, UpdateRequest model);
    //Task Delete(int id);
}

public class UserService : IUserService
{
    private List<User> _users = new List<User>
    {
        new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", Balance = 1500.0 }
    };

    private readonly IJwtUtils _jwtUtils;

    private readonly string _apiRoot;

    public UserService(IJwtUtils jwtUtils, ApiSettings apiSettings)
    {
        _jwtUtils = jwtUtils;
        _apiRoot = apiSettings.Database;
    }

    public async Task<string> Authenticate(AuthenticateRequest model)
    {
        var apiUrl = _apiRoot + "/api/User/getuserid";
        var response = await CreatePostRequest(apiUrl, model);

        if (!response.IsSuccessStatusCode) return null;

        var userId = await response.Content.ReadAsStringAsync();

        var token = _jwtUtils.GenerateJwtToken(userId);
        return token;
    }

    public async Task<bool> Register(RegisterModel model)
    {
        var apiUrl = _apiRoot + "/api/User/register";
        var response = await CreatePostRequest(apiUrl, model);
        return response.IsSuccessStatusCode;
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    private async Task<HttpResponseMessage> CreatePostRequest(string url, object data)
    {
        using (var client = new HttpClient())
        {
            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            return response;
        }
    }

    //public async Task Create(CreateRequest model)
    //{
    //    if (await _userRepository.GetByEmail(model.Email!) != null)
    //        throw new AppException("User with the email '" + model.Email + "' already exists");

    //    var user = _mapper.Map<User>(model);

    //    user.PasswordHash = BCrypt.HashPassword(model.Password);

    //    await _userRepository.Create(user);
    //}

    //public async Task Update(int id, UpdateRequest model)
    //{
    //    var user = await _userRepository.GetById(id);

    //    if (user == null)
    //        throw new KeyNotFoundException("User not found");

    //    var emailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;
    //    if (emailChanged && await _userRepository.GetByEmail(model.Email!) != null)
    //        throw new AppException("User with the email '" + model.Email + "' already exists");

    //    if (!string.IsNullOrEmpty(model.Password))
    //        user.PasswordHash = BCrypt.HashPassword(model.Password);

    //    _mapper.Map(model, user);

    //    await _userRepository.Update(user);
    //}

    //public async Task Delete(int id)
    //{
    //    await _userRepository.Delete(id);
    //}

    //public async Task AddMoney(int userId, double amount, string currency)
    //{
    //    var user = await _userRepository.GetById(userId);

    //    if (user == null)
    //        throw new KeyNotFoundException("User not found");

    //    if (currency != "USD")
    //    {
    //        double conversionRate = await _exchangeRateService.GetExchangeRate(currency, "USD");

    //        amount *= conversionRate;
    //    }

    //    user.Balance += amount;

    //    UpdateTier(user);

    //    await _userRepository.Update(user);
    //}

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

    Task<IEnumerable<User>> IUserService.GetAll()
    {
        throw new NotImplementedException();
    }

    //Task<User> IUserService.GetById(int id)
    //{
    //    throw new NotImplementedException();
    //}
}

