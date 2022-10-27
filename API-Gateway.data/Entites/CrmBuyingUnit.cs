using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmBuyingUnit
    {
        public int BuyingUnitInternalKey { get; set; }
        public string? LastName { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? HomePhone { get; set; }
        public string? EmailAddress { get; set; }
        public bool? IsEmployee { get; set; }
        public bool? DisableReward { get; set; }
        public string? Remarks { get; set; }
        public int ExportedRowVersion { get; set; }
        public int RecordVersion { get; set; }
        public string? ExternalBuyingUnit { get; set; }
        public string? AddressKana { get; set; }
        public short? MatrixMemberId { get; set; }
        public string? StreetNum { get; set; }
        public bool? SendEmail { get; set; }
        public string? PhonePrefix { get; set; }
        public string? Pobox { get; set; }
        public int? VirtualBuyingUnitInternalKey { get; set; }
        public int? ClubInternalKey { get; set; }
        public string? Extension { get; set; }
        public DateTime? StartDate { get; set; }
        public string? ExternalSource { get; set; }
        public byte? SourceId { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? County { get; set; }
        public string? HouseName { get; set; }
    }
}
