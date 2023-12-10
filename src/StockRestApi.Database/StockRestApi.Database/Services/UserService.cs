using Microsoft.AspNetCore.Identity;
using StockRestApi.Database.Data;
using StockRestApi.Database.Services.Contracts;

namespace StockRestApi.Database.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                return false;
            }

            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            await _userManager.CreateAsync(user, password);

            return true;
        }

        public async Task<bool> DoesUserExistAsync(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser == null)
            {
                return false;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, password);
            return isPasswordValid;
        }
    }
}
