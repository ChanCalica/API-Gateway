using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyOnlineWSUtilities.Models
{
    public class SingleTransactionModel
    {
        [JsonProperty("Transactions")]
        public SingleTransactions Transactions { get; set; }

    }

    public class SingleTransactions
    {
        [JsonProperty("Transaction")]
        public SingleTransaction Transaction { get; set; }
    }

    public class SingleTransaction
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
