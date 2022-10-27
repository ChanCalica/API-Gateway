using Azure;
using Azure.Data.Tables;

namespace API_Gateway.Models.Request
{
    public class MemberEntityRequest : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string EndPoint { get; set; }
        public string Method { get; set; }
        public string RequestHeader { get; set; }
        public string RequestBody { get; set; }
        public string StatusCode { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
