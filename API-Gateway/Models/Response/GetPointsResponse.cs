using Newtonsoft.Json;

namespace API_Gateway.Models.Response
{
    public class GetPointsResponse
    {
        [JsonIgnore]
        public string CardId { get; set; }
        [JsonProperty("balance")]
        public string Balance { get; set; }
        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }
    }
}
