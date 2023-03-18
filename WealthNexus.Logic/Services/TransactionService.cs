using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WealthNexus.Common.Models;
using WealthNexus.Logic.Interfaces;
using Transaction = WealthNexus.Common.Models.Transaction;
using Container = Microsoft.Azure.Cosmos.Container;

namespace WealthNexus.Logic.Services {
    public class TransactionService : ITransactionService
    {
        
        private Microsoft.Azure.Cosmos.Container _container;

        public TransactionService(Container container)
        {
            _container = container;
        }
        public async Task<Transaction?> GetTransactionAsync(string id)
        {
            try {
                ItemResponse<Transaction> response = await _container.ReadItemAsync<Transaction>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) {
                return null;
            }
        }

        public async Task<List<Transaction>> GetAccountTransactionsAsync(string accountId)
        {
            var responseList = new List<Transaction>();

            try {
                var parameterizedQuery = new QueryDefinition(
                        query: "SELECT * FROM transactions t WHERE t.accountId = @partitionKey"
                    )
                    .WithParameter("@partitionKey", accountId);

                using FeedIterator<Transaction> filteredFeed = _container.GetItemQueryIterator<Transaction>(
                    queryDefinition: parameterizedQuery
                );

                while (filteredFeed.HasMoreResults) {
                    FeedResponse<Transaction> response = await filteredFeed.ReadNextAsync();

                    // Iterate query results
                    foreach (Transaction item in response) {
                        responseList.Add(item);
                    }
                }
                return responseList;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) {
                return null;
            }
        }

        public async Task<Transaction?> CreateTransactionAsync(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid().ToString();
            await _container.CreateItemAsync<Transaction>(transaction, new PartitionKey(transaction.Id));
            return transaction;
        }

        public async Task DeleteTransactionAsync(string id)
        {
            var deleteResponse = await _container.DeleteItemAsync<Transaction>("id", new PartitionKey());
        }
    }
}
