using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmMemberLookup
    {
        public int? MemberInternalKey { get; set; }
        public int? BuyingUnitInternalKey { get; set; }
        public int? RecordVersion { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? ExternalMemberKey { get; set; }
    }
}
