using Microsoft.AspNetCore.Mvc;

using StockRestApi.Database.Data.Entities;
using StockRestApi.Database.Models.Account;
using StockRestApi.Database.Services.Contracts;

namespace StockRestApi.Database.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account != null)
            {
                return Ok(account);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountBaseModel account)
        {
            if (account == null)
            {
                return BadRequest("No account provided");
            }

            var result = await _accountService.CreateAsync(account);
            if (result)
            {
                return Ok();
            }

            return BadRequest("Could not create account.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(AccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No account provided");
            }

            var result = await _accountService.UpdateAsync(model);

            if (result)
            {
                return Ok();
            }

            return BadRequest("Could not update account.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountService.DeleteAsync(id);

            if (result)
            {
                return Ok();
            }
                
            return NotFound();
        }


    }
}
