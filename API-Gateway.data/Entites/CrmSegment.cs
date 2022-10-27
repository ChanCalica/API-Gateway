using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmSegment
    {
        public short MatrixMemberId { get; set; }
        public int ClubInternalKey { get; set; }
        public int SegmentInternalKey { get; set; }
        public int SegmentId { get; set; }
        public string SegmentDescription { get; set; } = null!;
        public bool Status { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? PublicationStatus { get; set; }
        public byte[]? PublicationRowVersion { get; set; }
        public byte? Type { get; set; }
        public short? GroupId { get; set; }
        public byte? SubType { get; set; }
        public bool IsShared { get; set; }
        public short DeactivatedSegmentUpdate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? SegmentValidityPeriodUpdate { get; set; }
        public bool RemoveWhenExpired { get; set; }
        public bool? AllowManualAttachment { get; set; }
        public int? ControlGroupSegmentInternalKey { get; set; }
        public bool UploadInTransactionsSummary { get; set; }
        public bool ReasonsRequiredForManualChanges { get; set; }
        public bool? HomeStoreMandatory { get; set; }
        public bool CentralManagement { get; set; }
        public bool? SyncToMsg12 { get; set; }
    }
}
