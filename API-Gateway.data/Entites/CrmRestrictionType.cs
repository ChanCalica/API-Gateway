using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmRestrictionType
    {
        public short RestrictionId { get; set; }
        public string RestrictionDescription { get; set; } = null!;
        public byte RestrictionType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefaultinAutoAdded { get; set; }
        public bool IsManual { get; set; }
        public bool ReadOnly { get; set; }
        public byte CardsStatusEffect { get; set; }
        public bool IsReversible { get; set; }
        public bool IsActiveStatus { get; set; }
        public bool DefaultStatusForAutomaticDisableInIssuing { get; set; }
        public bool? IsDefaultinMs { get; set; }
    }
}
