using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmClubcard
    {
        public short MatrixMemberId { get; set; }
        public string ClubCardId { get; set; } = null!;
        public int ClubInternalKey { get; set; }
        public int? MemberInternalKey { get; set; }
        public short? RestrictionId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool PublicationStatus { get; set; }
        public short? BarcodeId { get; set; }
        public DateTime? LastUpdateStatusDate { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public byte? SourceId { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
