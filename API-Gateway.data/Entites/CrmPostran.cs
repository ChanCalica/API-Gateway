using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmPostran
    {
        public int PostranInternalKey { get; set; }
        public short MatrixMemberId { get; set; }
        public short StoreInternalKey { get; set; }
        public DateTime PosDateTime { get; set; }
        public short PosId { get; set; }
        public int TranId { get; set; }
        public int BuyingUnitInternalKey { get; set; }
        public string ClubCardId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public decimal? SalesAmount { get; set; }
        public short TicketStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public bool? IsHomeStore { get; set; }
        public int UpdatedBy { get; set; }
        public byte? ReasonCode { get; set; }
        public bool? IsTransactionVoid { get; set; }
        public string? MessageSource { get; set; }
        public string? MessageSignature { get; set; }
        public bool? SignatureValidationStatus { get; set; }
        public bool? Locked { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public string? OriginalClubCardId { get; set; }
        public string? ExternalTranId { get; set; }
        public int? EndTransactionTrustLevel { get; set; }
        public byte? RescanStatus { get; set; }
        public decimal? AmountDiff { get; set; }
        public byte? TrustLevelCalcSource { get; set; }
    }
}
