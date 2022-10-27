using Newtonsoft.Json;

namespace API_Gateway.Models.Response
{
    public class Member
    {
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [JsonProperty("mobileNumber")]
        public int? MobileNumber { get; set; }
        [JsonProperty("birthDate")]
        public DateTime? BirthDate { get; set; }
        [JsonProperty("memberInternalKey")]
        public int MemberInternalKey { get; set; }
        [JsonProperty("externalMemberKey")]
        public string? ExternalMemberKey { get; set; }
        [JsonProperty("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }
    }
}
