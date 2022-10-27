using Newtonsoft.Json;
using System.ComponentModel;

namespace API_Gateway.Models.Response
{
    public class Profile
    {
        [DefaultValue("")]
        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string? FirstName { get; set; }
        [DefaultValue("")]
        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string? LastName { get; set; }
        [DefaultValue("")]
        [JsonProperty("middleInitial", NullValueHandling = NullValueHandling.Ignore)]
        public string? MiddleInitial { get; set; }
        [DefaultValue("")]
        [JsonProperty("middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string? MiddleName { get; set; }
        [DefaultValue("")]
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string? Gender { get; set; }
        [DefaultValue("")]
        [JsonProperty("civilStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string? CivilStatus { get; set; }
        [DefaultValue("")]
        [JsonProperty("mobileNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? MobileNumber { get; set; }
        [DefaultValue("")]
        [JsonProperty("altMobileNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string? AltMobileNumber { get; set; }
        [JsonProperty("birthDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? BirthDate { get; set; }
        [DefaultValue("")]
        [JsonProperty("emailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string? EmailAddress { get; set; }
        [DefaultValue("")]
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string? Country { get; set; }
        [DefaultValue("")]
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string? City { get; set; }
        [DefaultValue("")]
        [JsonProperty("province", NullValueHandling = NullValueHandling.Ignore)]
        public string? Province { get; set; }
        [DefaultValue("")]
        [JsonProperty("street1", NullValueHandling = NullValueHandling.Ignore)]
        public string? Street1 { get; set; }
        [DefaultValue("")]
        [JsonProperty("street2", NullValueHandling = NullValueHandling.Ignore)]
        public string? Street2 { get; set; }
        [DefaultValue("")]
        [JsonProperty("streetNum", NullValueHandling = NullValueHandling.Ignore)]
        public string? StreetNum { get; set; }
        [DefaultValue("")]
        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }
        [DefaultValue("")]
        [JsonProperty("poBox", NullValueHandling = NullValueHandling.Ignore)]
        public string POBox { get; set; }
        [DefaultValue("")]
        [JsonProperty("homePhone", NullValueHandling = NullValueHandling.Ignore)]
        public string HomePhone { get; set; }
    }
}
