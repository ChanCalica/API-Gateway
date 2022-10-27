using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmBuyingUnitHistory
    {
        public int BuyingUnitHistoryId { get; set; }
        public int BuyingUnitInternalKey { get; set; }
        public byte EventId { get; set; }
        public string? SourceClubCardId { get; set; }
        public string? TargetClubCardId { get; set; }
        public int? SourceBuyingUnitInternalKey { get; set; }
        public int? TargetBuyingUnitInternalKey { get; set; }
        public int? SourceMemberInternalKey { get; set; }
        public int? TargetMemberInternalKey { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? PosTranInternalKey { get; set; }
    }
}
