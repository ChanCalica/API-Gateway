using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmMember
    {
        public int MemberInternalKey { get; set; }
        public int BuyingUnitInternalKey { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleInitial { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? AutoAdded { get; set; }
        public string? DriversLicense { get; set; }
        public string? NationalInsuranceNumber { get; set; }
        public string? Remarks { get; set; }
        public int ExportedRowVersion { get; set; }
        public int RecordVersion { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public short? Gender { get; set; }
        public string? Passport { get; set; }
        public short? Title { get; set; }
        public string? ExternalMemberKey { get; set; }
        public DateTime? StartDate { get; set; }
        public byte? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsMainMember { get; set; }
        public byte RedemptionPrivileges { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string? AdressNormalizationUpdate { get; set; }
        public string? SpouseLastName { get; set; }
        public byte? Anonimity { get; set; }
        public byte? NumberOfFamilyMembers { get; set; }
        public byte? PostOption { get; set; }
        public short RestrictionId { get; set; }
        public short? LanguageId { get; set; }
        public short? ReceiptLayoutId { get; set; }
        public DateTime? LastUpdatedDateForExport { get; set; }
        public string? CommercialDriversLicense { get; set; }
        public string? CompanyName { get; set; }
        public string? AdditionalFirstName { get; set; }
        public string? AdditionalLastName { get; set; }
        public string? EmailAddress { get; set; }
        public byte? SourceId { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
