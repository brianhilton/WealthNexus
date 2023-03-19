using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthNexus.Common.Models;

namespace WealthNexus.Logic.Interfaces {
    public interface IAccountService {

        public Task<Account?> GetAccountAsync(string accountId);

        public Task<Account?> CreateAccountAsync(Account account);

        public Task<Account?> UpdateAccountAsync(Account account);

        public Task<Account?> GetUserByName(string username);
    }
}
