using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmPosaccountsActivity
    {
        public int PosTranInternalKey { get; set; }
        public int AccountInternalKey { get; set; }
        public int BuyingUnitInternalKey { get; set; }
        public short MatrixMemberId { get; set; }
        public DateTime PosDateTime { get; set; }
        public DateTime ProcessDate { get; set; }
        public decimal? EarnValue { get; set; }
        public decimal? RedeemValue { get; set; }
        public decimal? InitialValue { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public byte? ReasonCode { get; set; }
        public string? Remarks { get; set; }
        public int? SourcePosTranInternalKey { get; set; }
        public short? ActivityFlags { get; set; }
        public string? ExternalReferenceId { get; set; }
    }
}
