using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using StockRestApi.Database.Data.Entities;
using StockRestApi.Database.Data.Enums;

namespace StockRestApi.Database.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }
        public DbSet<StockPortfolio> StockPortfolios { get; set; }
        public DbSet<StockWatchlist> StockWatchlists { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
    }

    public static class Seeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate(); // Ensure the database is created

                // Seed users
                if (!context.Users.Any())
                {
                    var users = new List<IdentityUser>
                    {
                        new IdentityUser { UserName = "user1@example.com", Email = "user1@example.com" },
                        new IdentityUser { UserName = "user2@example.com", Email = "user2@example.com" }
                        // Add more users as needed
                    };

                    context.Users.AddRange(users);
                    context.SaveChanges();
                }

                // Seed stocks
                if (!context.Stocks.Any())
                {
                    var stocks = new List<Stock>
                {
                    new Stock { Name = "TechCorp", Symbol = "TC", Price = 150.50M },
                    new Stock { Name = "HealthCare Inc.", Symbol = "HCI", Price = 300.00M }
                    // Add more stocks as needed
                };

                    context.Stocks.AddRange(stocks);
                    context.SaveChanges();
                }

                // Seed accounts and related entities
                if (!context.Accounts.Any())
                {
                    var user = context.Users.First();
                    var stock = context.Stocks.First();

                    var account = new Account { UserId = user.Id, Balance = 10000M };
                    context.Accounts.Add(account);
                    context.SaveChanges();

                    var portfolio = new StockPortfolio { AccountId = account.Id, StockId = stock.Id, Quantity = 100 };
                    context.StockPortfolios.Add(portfolio);
                    context.SaveChanges();

                    var watchlist = new StockWatchlist { AccountId = account.Id, StockId = stock.Id };
                    context.StockWatchlists.Add(watchlist);
                    context.SaveChanges();

                    var transaction = new TransactionHistory
                    {
                        StockPortfolioId = portfolio.Id,
                        PricePerShare = stock.Price,
                        Quantity = portfolio.Quantity,
                        TotalPrice = (decimal)portfolio.Quantity * stock.Price,
                        TradeAction = TradeAction.Buy
                    };
                    context.TransactionHistory.Add(transaction);
                    context.SaveChanges();
                }
            }
        }
    }
}