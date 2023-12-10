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

        public async Task<string> RegisterAsync(string username, string email, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                return null;
            }

            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            await _userManager.CreateAsync(user, password);

            return user.Id;
        }

        public async Task<string> GetUserId(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser == null)
            {
                return null;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, password);
            if (!isPasswordValid)
            {
                return null;
            }

            return existingUser.Id;
        }
    }
}
