using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction = WealthNexus.Common.Models.Transaction;

namespace WealthNexus.Logic.Interfaces {
    public interface ITransactionService
    {
        public Task<Transaction?> GetTransactionAsync(string id);

        public Task<List<Transaction>> GetAccountTransactionsAsync(string id);

        public Task<Transaction?> CreateTransactionAsync(Transaction transaction);

        public Task DeleteTransactionAsync(string id);

    }
}
