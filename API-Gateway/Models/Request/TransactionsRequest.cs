using Newtonsoft.Json;

namespace API_Gateway.Models.Request
{
    public class TransactionsRequest
    {
        [JsonProperty("@MaxRows")]
        public string? MaxRows { get; set; }
        [JsonProperty("@StartDate")]
        public string? StartDate { get; set; }
        [JsonProperty("@EndDate")]
        public string? EndDate { get; set; }

      

    }

    public class FilteredTransactions
    {
        public TransactionsRequest Filter { get; set; }

        public FilteredTransactions()
        {
            Filter = new TransactionsRequest();

        }
    }
}
