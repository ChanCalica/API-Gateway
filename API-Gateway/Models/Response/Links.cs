using Newtonsoft.Json;

namespace API_Gateway.Models.Response
{
    public class Links
    {
        [JsonProperty("memberInfo")]
        public MemberInfo MemberInfo { get; set; } 
    }

    public class MemberInfo
    {
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
