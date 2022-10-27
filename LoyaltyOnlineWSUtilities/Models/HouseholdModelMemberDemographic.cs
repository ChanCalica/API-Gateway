
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static API_Gateway.Common.Logics.JSONUtilities;

namespace LoyaltyOnlineWSUtilities.Models
{// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class XmlMemberDemographic
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }

    public class CardMemberDemographic
    {
        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("@CardStatus")]
        public string CardStatus { get; set; }

        [JsonProperty("@IssueDate")]
        public DateTime IssueDate { get; set; }

        [JsonProperty("@ExpirationDate")]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("@EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("@BarcodeId")]
        public string BarcodeId { get; set; }
    }

    public class CardsMemberDemographic
    {
        public CardMemberDemographic Card { get; set; }
    }

    public class AttributeMemberDemographic
    {
        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("@Value")]
        public string Value { get; set; }

        [JsonProperty("@DataType")]
        public string DataType { get; set; }
    }

    public class MemberAttributesMemberDemographic
    {
        public List<AttributeMemberDemographic> Attribute { get; set; }
    }

    public class MemberMemberDemographic
    {
        [JsonProperty("@MemberInternalKey")]
        public string MemberInternalKey { get; set; }

        [JsonProperty("@MemberExternalId")]
        public string MemberExternalId { get; set; }

        [JsonProperty("@IsMainMember")]
        public string IsMainMember { get; set; }

        [JsonProperty("@LastName")]
        public string LastName { get; set; }

        [JsonProperty("@FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("@AdditionalFirstName")]
        public string AdditionalFirstName { get; set; }

        [JsonProperty("@AdditionalLastName")]
        public string AdditionalLastName { get; set; }

        [JsonProperty("@MiddleInitial")]
        public string MiddleInitial { get; set; }

        [JsonProperty("@BirthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("@DriversLicense")]
        public string DriversLicense { get; set; }

        [JsonProperty("@NationalInsuranceNumber")]
        public string NationalInsuranceNumber { get; set; }

        [JsonProperty("@Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("@MobilePhoneNumber")]
        public string MobilePhoneNumber { get; set; }

        [JsonProperty("@WorkPhoneNumber")]
        public string WorkPhoneNumber { get; set; }

        [JsonProperty("@Gender")]
        public string Gender { get; set; }

        [JsonProperty("@Passport")]
        public string Passport { get; set; }

        [JsonProperty("@StartDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("@EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("@RedemptionPrivileges")]
        public string RedemptionPrivileges { get; set; }

        [JsonProperty("@LanguageId")]
        public string LanguageId { get; set; }

        [JsonProperty("@NumberOfFamilyMembers")]
        public string NumberOfFamilyMembers { get; set; }

        [JsonProperty("@Anonimity")]
        public string Anonimity { get; set; }

        [JsonProperty("@SpouseLastName")]
        public string SpouseLastName { get; set; }

        [JsonProperty("@MemberStatus")]
        public string MemberStatus { get; set; }

        [JsonProperty("@ReceiptLayoutId")]
        public string ReceiptLayoutId { get; set; }

        [JsonProperty("@AdressNormalizationUpdate")]
        public string AdressNormalizationUpdate { get; set; }

        [JsonProperty("@UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        [JsonProperty("@CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("@EMailAddress")]
        public string EMailAddress { get; set; }
        public CardsMemberDemographic Cards { get; set; }
        public MemberAttributesMemberDemographic MemberAttributes { get; set; }
    }

    public class MembersMemberDemographic
    {
        [JsonConverter(typeof(SingleOrArrayConverter<MemberMemberDemographic>))]
        public List<MemberMemberDemographic> Member { get; set; }
    }

    public class HouseholdMemberDemographic
    {
        [JsonProperty("@BuyingUnitInternalKey")]
        public int BuyingUnitInternalKey { get; set; }

        [JsonProperty("@HouseHoldExternalId")]
        public string HouseHoldExternalId { get; set; }

        [JsonProperty("@Country")]
        public string Country { get; set; }

        [JsonProperty("@State")]
        public string State { get; set; }

        [JsonProperty("@City")]
        public string City { get; set; }

        [JsonProperty("@Street1")]
        public string Street1 { get; set; }

        [JsonProperty("@Street2")]
        public string Street2 { get; set; }

        [JsonProperty("@StreetNum")]
        public string StreetNum { get; set; }

        [JsonProperty("@PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("@POBox")]
        public string POBox { get; set; }

        [JsonProperty("@PhonePrefix")]
        public string PhonePrefix { get; set; }

        [JsonProperty("@HomePhone")]
        public string HomePhone { get; set; }

        [JsonProperty("@EMailAddress")]
        public string EMailAddress { get; set; }

        [JsonProperty("@SendEmail")]
        public string SendEmail { get; set; }

        [JsonProperty("@County")]
        public string County { get; set; }

        [JsonProperty("@HouseName")]
        public string HouseName { get; set; }
        public MembersMemberDemographic Members { get; set; }
    }

    public class HouseholdModelMemberDemographic
    {
        [JsonProperty("?xml")]
        public XmlMemberDemographic Xml { get; set; }
        public HouseholdMemberDemographic HouseHold { get; set; }
    }
}
