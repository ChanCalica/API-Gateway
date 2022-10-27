using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmLoyaltyDocument
    {
        public int DocumentInternalKey { get; set; }
        public short IssueMatrixMemberId { get; set; }
        public short IssueStoreInternalKey { get; set; }
        public int IssuedBuyingUnitInternalKey { get; set; }
        public string? BusinessId { get; set; }
        public byte Type { get; set; }
        public string Barcode { get; set; } = null!;
        public byte Status { get; set; }
        public int? PromotionHeaderId { get; set; }
        public decimal Value { get; set; }
        public short? TenderGroup { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? RedemptionType { get; set; }
        public byte? RedemptionLocation { get; set; }
        public decimal IssuedQty { get; set; }
        public decimal RedeemedValue { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreationDateTime { get; set; }
        public byte? RedemptionMode { get; set; }
        public int? DocumentId { get; set; }
        public short? BarcodeId { get; set; }
        public short? MatrixMemberId { get; set; }
        public int? ClubInternalKey { get; set; }
        public DateTime? HousekeepingDate { get; set; }
    }
}
