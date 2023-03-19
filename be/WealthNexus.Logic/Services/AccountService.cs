using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WealthNexus.Common.Models;
using WealthNexus.Logic.Interfaces;
using Microsoft.Azure.Cosmos;

namespace WealthNexus.Logic.Services {
    public class AccountService : IAccountService {
        
        private Container _container;

        public AccountService(Container container) { _container = container; }
        public async Task<Account?> GetAccountAsync(string accountId)
        {
            try
            {
                ItemResponse<Account> response = await _container.ReadItemAsync<Account>(accountId, new PartitionKey(accountId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        
        public async Task<Account?> CreateAccountAsync(Account account)
        {
            account.Id = Guid.NewGuid().ToString();
            await _container.CreateItemAsync<Account>(account, new PartitionKey(account.Id));
            return account;
        }

        public async Task<Account?> UpdateAccountAsync(Account account)
        {
            await _container.UpsertItemAsync<Account>(account, new PartitionKey(account.Id));
            return account;
        }

        public async Task<Account?> GetUserByName(string username)
        {
            Account? responseAccount = null;

            try
            {
                var parameterizedQuery = new QueryDefinition(
                        query: "SELECT * FROM accounts a WHERE a.Username = @partitionKey"
                    )
                    .WithParameter("@partitionKey", username);

                using FeedIterator<Account> filteredFeed = _container.GetItemQueryIterator<Account>(
                    queryDefinition: parameterizedQuery
                );

                while (filteredFeed.HasMoreResults)
                {
                    FeedResponse<Account> response = await filteredFeed.ReadNextAsync();

                    // Iterate query results
                    responseAccount = response.FirstOrDefault();
                }
                return responseAccount;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
