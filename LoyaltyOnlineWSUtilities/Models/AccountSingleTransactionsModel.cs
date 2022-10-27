using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyOnlineWSUtilities.Models
{
    public class AccountSingleTransactionsModel
    {
        [JsonProperty("Transactions")]
        public AccountSingleTransactions Transactions { get; set; }
    }

    public class AccountSingleTransactions
    {
        [JsonProperty("Transaction")]
        public AccountSingleTransaction Transaction { get; set; }

    }

    public class AccountSingleTransaction
    {
        [JsonProperty("@RetailerId")]
        public string? RetailerId { get; set; }

        [JsonProperty("@StoreId")]
        public string? StoreId { get; set; }

        [JsonProperty("@StoreName")]
        public string? StoreName { get; set; }

        [JsonProperty("@PosDateTime")]
        public string? PosDateTime { get; set; }

        [JsonProperty("@PosId")]
        public string? PosId { get; set; }

        [JsonProperty("@TranId")]
        public string? TranId { get; set; }

        [JsonProperty("@StartDateTime")]
        public string? StartDateTime { get; set; }

        [JsonProperty("@ClubCardId")]
        public string? ClubCardId { get; set; }

        [JsonProperty("@TransactionTotalAmount")]
        public string? TransactionTotalAmount { get; set; }

        [JsonProperty("@IsTransactionVoid")]
        public string? IsTransactionVoid { get; set; }

        [JsonProperty("@ReasonDescription")]
        public string? ReasonDescription { get; set; }

        [JsonProperty("@ReasonCode")]
        public string? ReasonCode { get; set; }

        [JsonProperty("@ReasonName")]
        public string? ReasonName { get; set; }

        [JsonProperty("@ExternalTranId")]
        public string? ExternalTranId { get; set; }

        [JsonProperty("MemberAccount")]
        public SingleMemberAccount MemberAccount { get; set; }

    }

    public class SingleMemberAccount
    {
        [JsonProperty("@Id")]
        public string? Id { get; set; }

        [JsonProperty("@EarnValue")]
        public string? EarnValue { get; set; }

        [JsonProperty("@RedeemValue")]
        public string? RedeemValue { get; set; }

        [JsonProperty("@InitialValue")]
        public string? InitialValue { get; set; }

        [JsonProperty("@RewardUsedAmount")]
        public string? RewardUsedAmount { get; set; }

        [JsonProperty("@RewardStatus")]
        public string? RewardStatus { get; set; }

    }
}
