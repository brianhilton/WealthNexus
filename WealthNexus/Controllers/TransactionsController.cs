using System.Drawing.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WealthNexus.Common.Models;
using WealthNexus.Logic.Interfaces;

namespace WealthNexus.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase {

        private readonly ITransactionService _transactions;
        private readonly IAccountService _accounts;
        private readonly ILogger _log;

        public TransactionsController(ITransactionService transactions, ILoggerFactory loggerFactory, IAccountService accounts)
        {
            _transactions = transactions;
            _log = loggerFactory.CreateLogger<TransactionsController>();
            _accounts = accounts;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(string id)
        {
            var transaction = await _transactions.GetTransactionAsync(id);

            if (transaction == null) return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction transaction)
        {
            var user = await _accounts.GetAccountAsync(transaction.AccountId);
            if(user == null) return BadRequest();

            user.AccountBalance += transaction.Amount;
            await _accounts.UpdateAccountAsync(user);

            var newTransaction = await _transactions.CreateTransactionAsync(transaction);
            if (newTransaction == null) return BadRequest();
            return Ok(newTransaction);
        }

        [HttpGet("account/{id}")]
        public async Task<IActionResult> GetAccountTransaction(string id) {
            var transaction = await _transactions.GetTransactionAsync(id);

            return Ok(transaction);
        }
    }
}
