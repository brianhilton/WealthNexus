using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WealthNexus.Common.Models;
using WealthNexus.Logic.Interfaces;

namespace WealthNexus.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accounts;
        private readonly ILogger _log;
        public AccountsController(IAccountService accounts, ILoggerFactory loggerFactory)
        {
            _accounts = accounts;
            _log = loggerFactory.CreateLogger<AccountsController>();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            var newAccount = await _accounts.CreateAccountAsync(account);

            return Ok(newAccount);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(string id)
        {
            var account = await _accounts.GetAccountAsync(id);

            if (account == null) return NotFound();

            return Ok(account);
        }
    }
}
