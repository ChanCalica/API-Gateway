using Newtonsoft.Json;

namespace API_Gateway.Models.Request
{
    public class BaseRequest
    {
        [JsonIgnore]
        public string? CurrentUrl { get; set; }
    }
}
