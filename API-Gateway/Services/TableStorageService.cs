using API_Gateway.Models.Request;
using API_Gateway.Services.Interface;
using Azure.Data.Tables;

namespace API_Gateway.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string TableName = "APILogsDev";
        private readonly IConfiguration _configuration;
        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["ConnectionStrings:AzureBlobStorage"]);
            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }

        public async Task<MemberEntityRequest> GetEntityAsync(string PartitionKey, string RowKey)
        {
            var tableClient = await GetTableClient();
            var response = await tableClient.GetEntityAsync<MemberEntityRequest>(PartitionKey, RowKey);
            return response;
        }

        public async Task<MemberEntityRequest> UpsertEntityAsync(MemberEntityRequest entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(string PartitionKey, string RowKey)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(PartitionKey, RowKey);
        }
    }
}
