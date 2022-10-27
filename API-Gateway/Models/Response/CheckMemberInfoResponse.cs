using Newtonsoft.Json;

namespace API_Gateway.Models.Response
{
    public class CheckMemberInfoResponse
    {
        [JsonProperty("resultSet")]
        public string ResultSet { get; set; }
        [JsonProperty("members")]
        public List<MemberInfoResponse> Members { get; set; }

        public CheckMemberInfoResponse()
        {
            ResultSet = string.Empty;
            Members = new List<MemberInfoResponse>();
        }
    }
}
