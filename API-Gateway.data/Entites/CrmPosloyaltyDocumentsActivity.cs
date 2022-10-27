using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmPosloyaltyDocumentsActivity
    {
        public int PostranInternalKey { get; set; }
        public DateTime PosDateTime { get; set; }
        public int DocumentInternalKey { get; set; }
        public int BuyingUnitInternalKey { get; set; }
        public byte Action { get; set; }
        public byte Qty { get; set; }
    }
}
