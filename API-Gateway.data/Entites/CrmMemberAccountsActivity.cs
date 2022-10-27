using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmMemberAccountsActivity
    {
        public int MemberInternalKey { get; set; }
        public int AccountInternalKey { get; set; }
        public int BuyingUnitInternalKey { get; set; }
        public decimal TotalAccumulated { get; set; }
        public decimal TotalRedeemed { get; set; }
        public bool IsArchivator { get; set; }
    }
}
