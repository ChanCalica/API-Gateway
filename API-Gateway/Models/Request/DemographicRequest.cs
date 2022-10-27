using Newtonsoft.Json;

namespace API_Gateway.Models.Request
{
    public class PartnerCode
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("value")]
        public string? Value { get; set; }
    }

    public class DemographicMemberRequest
    {
        [JsonProperty("isMainMember")]
        public string? IsMainMember { get; set; }
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("additionalFirstName")]
        public string? AdditionalFirstName { get; set; }
        [JsonProperty("additionalLastName")]
        public string? AdditionalLastName { get; set; }
        [JsonProperty("middleInitial")]
        public string? MiddleInitial { get; set; }
        [JsonProperty("middleName")]
        public string? MiddleName { get; set; }
        [JsonProperty("birthDate")]
        public string? BirthDate { get; set; }
        [JsonProperty("driversLicense")]
        public string? DriversLicense { get; set; }
        [JsonProperty("nationalInsuranceNumber")]
        public string? NationalInsuranceNumber { get; set; }
        [JsonProperty("remarks")]
        public string? Remarks { get; set; }
        [JsonProperty("mobilePhoneNumber")]
        public string? MobilePhoneNumber { get; set; }
        [JsonProperty("altMobileNumber")]
        public string? AltMobileNumber { get; set; }
        [JsonProperty("workPhoneNumber")]
        public string? WorkPhoneNumber { get; set; }
        [JsonProperty("gender")]
        public string? Gender { get; set; }
        [JsonProperty("passport")]
        public string? Passport { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("effectiveDate")]
        public DateTime? EffectiveDate { get; set; }
        [JsonProperty("redemptionPrivileges")]
        public string? RedemptionPrivileges { get; set; }
        [JsonProperty("languageId")]
        public string? LanguageId { get; set; }
        [JsonProperty("numberOfFamilyMembers")]
        public string? NumberOfFamilyMembers { get; set; }
        [JsonProperty("anonimity")]
        public string? Anonimity { get; set; }
        [JsonProperty("spouseLastName")]
        public string? SpouseLastName { get; set; }
        [JsonProperty("memberStatus")]
        public string? MemberStatus { get; set; }
        [JsonProperty("civilStatus")]
        public string? CivilStatus { get; set; }
        [JsonProperty("receiptLayoutId")]
        public string? ReceiptLayoutId { get; set; }
        [JsonProperty("adressNormalizationUpdate")]
        public string? AdressNormalizationUpdate { get; set; }
        [JsonProperty("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [JsonProperty("companyName")]
        public string? CompanyName { get; set; }
        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }
        [JsonProperty("cardId")]
        public string? CardId { get; set; }
        [JsonProperty("memberAttribute")]
        public PartnerCode? MemberAttribute { get; set; }

    }

    public class DemographicRequest : BaseRequest
    {
        [JsonProperty("country")]
        public string? Country { get; set; }
        [JsonProperty("province")]
        public string? Province { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("street1")]
        public string? Street1 { get; set; }
        [JsonProperty("street2")]
        public string? Street2 { get; set; }
        [JsonProperty("streetNum")]
        public string? StreetNum { get; set; }
        [JsonProperty("postalCode")]
        public string? PostalCode { get; set; }
        [JsonProperty("poBox")]
        public string? POBox { get; set; }
        [JsonProperty("phonePrefix")]
        public string? PhonePrefix { get; set; }
        [JsonProperty("homePhone")]
        public string? HomePhone { get; set; }
        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }
        [JsonProperty("sendEmail")]
        public string? SendEmail { get; set; }
        [JsonProperty("county")]
        public string? County { get; set; }
        [JsonProperty("houseName")]
        public string? HouseName { get; set; }
        [JsonProperty("member")]
        public DemographicMemberRequest? Member { get; set; }

    }
}
