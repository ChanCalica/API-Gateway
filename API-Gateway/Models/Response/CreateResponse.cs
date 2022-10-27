using Newtonsoft.Json;

namespace API_Gateway.Models.Response
{
    public class CreateResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("member")]
        public CreatedMember Member { get; set; }

    }

    public class CreatedMember
    {
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("middleInitial")]
        public string? MiddleInitial { get; set; }
        [JsonProperty("middleName")]
        public string? MiddleName { get; set; }
        [JsonProperty("birthDate")]
        public string? BirthDate { get; set; }
        [JsonProperty("remarks")]
        public string? Remarks { get; set; }
        [JsonProperty("mobilePhoneNumber")]
        public string? MobilePhoneNumber { get; set; }
        [JsonProperty("altMobileNumber")]
        public string? AltMobileNumber { get; set; }
        [JsonProperty("gender")]
        public string? Gender { get; set; }
        [JsonProperty("companyName")]
        public string? CompanyName { get; set; }
        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }
        [JsonProperty("cardId")]
        public string? CardId { get; set; }
    }
}
