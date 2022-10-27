using Newtonsoft.Json;

namespace API_Gateway.Models.Request
{
    public class AdjustPointsRequest : BaseRequest
    {
        public string? RetailerId { get; set; }
        public string? PosId { get; set; }
        public string? CashierId { get; set; }
        [JsonIgnore]
        public string? CardId { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        public string? StoreId { get; set; }
        public int TransId { get; set; }
        public DateTime TransTime { get; set; }
        public string? EarnValue { get; set; }
        public string? TicketTotal { get; set; }
        public string? RedeemValue { get; set; }


    }
}
