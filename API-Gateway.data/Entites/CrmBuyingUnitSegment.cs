using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmBuyingUnitSegment
    {
        public int BuyingUnitInternalKey { get; set; }
        public int SegmentInternalKey { get; set; }
        public short MatrixMemberId { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? AttachmentSourceId { get; set; }
        public byte? SourceId { get; set; }
    }
}
