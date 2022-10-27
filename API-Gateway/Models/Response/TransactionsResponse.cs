using Newtonsoft.Json;

namespace API_Gateway.Models.Response
{
    public class TransactionsResponse
    {

        public MemberTransactions Transactions { get; set; }

    }

    public class MemberTransactions
    {
        public MemberTransaction  Transaction { get; set; }
    }

    public class MemberTransaction
    {

        [JsonProperty("@StoreId")]
        public string? StoreId { get; set; }
        [JsonProperty("@PosDateTime")]
        public string? PosDateTime { get; set; }
        [JsonProperty("@PosId")]
        public string? PosId { get; set; }
        [JsonProperty("@TransId")]
        public string? TransId { get; set; }
        [JsonProperty("@StartDateTime")]
        public string? StartDateTime { get; set; }
        [JsonProperty("@ClubCardId")]
        public string? ClubCardId { get; set; }
        [JsonProperty("@CreatedAt")]
        public string? CreatedAt { get; set; }
        [JsonProperty("@UpdatedBy")]
        public string? UpdatedBy { get; set; }
        [JsonProperty("@TotalAmount")]
        public string? TotalAmount { get; set; }
        [JsonProperty("@StoreName")]
        public string? StoreName { get; set; }
        [JsonProperty("@IsTransactionVoid")]
        public string? IsTransactionVoid { get; set; }


    }
}
