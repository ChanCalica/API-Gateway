using API_Gateway.Models.Request;

namespace API_Gateway.Services.Interface
{
    public interface ITableStorageService
    {
        Task<MemberEntityRequest> GetEntityAsync(string PartitionKey, string RowKey);
        Task<MemberEntityRequest> UpsertEntityAsync(MemberEntityRequest entity);
        Task DeleteEntityAsync(string PartitionKey, string RowKey);

    }
}
