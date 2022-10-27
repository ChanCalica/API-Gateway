using System;
using System.Collections.Generic;

namespace API_Gateway.data.Entites
{
    public partial class CrmBuyingUnitAccountsActivity
    {
        public int BuyingUnitInternalKey { get; set; }
        public int AccountInternalKey { get; set; }
        public decimal TotalAccumulated { get; set; }
        public decimal TotalRedeemed { get; set; }
        public DateTime? LastUpdate { get; set; }
        public decimal Balance { get; set; }
        public int? PublicationStatus { get; set; }
        public byte ArchiveStatus { get; set; }
        public int? LastEarnTranId { get; set; }
        public int? LastBurnTranId { get; set; }
        public DateTime? HousekeepingDate { get; set; }
        public decimal HousekeepingBalance { get; set; }
        public decimal HousekeepingTotalAccumulated { get; set; }
        public decimal HousekeepingTotalRedeemed { get; set; }
    }
}
