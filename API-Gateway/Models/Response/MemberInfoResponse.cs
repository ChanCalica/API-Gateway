using Newtonsoft.Json;
using System.ComponentModel;

namespace API_Gateway.Models.Response
{
    public class MemberInfoResponse
    {
        [JsonProperty("cardId")]
        public string CardId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("balance")]
        public string Balance { get; set; }
        [DefaultValue("")]
        [JsonProperty("tier", NullValueHandling = NullValueHandling.Ignore)]
        public string? Tier { get; set; }
        [JsonProperty("accountCreated")]
        public DateTime AccountCreated { get; set; }
        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }
        [JsonProperty("member")]
        public Profile Member { get; set; }
        [JsonProperty("_links")]
        public Links _Links { get; set; }
    }
}
