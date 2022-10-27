using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmBuyingUnitHistoryEvent
    {
        public byte EventId { get; set; }
        public string EventEntryId { get; set; } = null!;
    }
}
