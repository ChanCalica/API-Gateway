using Newtonsoft.Json;

namespace API_Gateway.Models.Request
{
    public class CheckMemberRequest : BaseRequest
    {
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
        [JsonProperty("birthDate")]
        public DateTime? BirthDate { get; set; }
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("tier")]
        public string? Tier { get; set; }
    }
}
