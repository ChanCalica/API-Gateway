namespace API_Gateway.Models.Request
{
    public class BatchPointsRequest
    {
        public string? RetailerId { get; set; }
        public List<BatchTransaction> Transactions { get; set; }

    }

    public class BatchTransaction
    {
        public string? CardId { get; set; }
        public string? StoreId { get; set; }
        public string? PosId { get; set; }
        public string? CashierId { get; set; }
        public int TransId { get; set; }
        public DateTime TransTime { get; set; }
        public string? EarnValue { get; set; }
        public string? TicketTotal { get; set; }

    }
}
