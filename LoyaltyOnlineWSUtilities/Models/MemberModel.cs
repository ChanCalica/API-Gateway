using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyOnlineWSUtilities.Models
{
    public class MemberModel
    {
        [JsonProperty("Member")]
        public SingleMemberDetails Member {get; set;}
    }

    public class SingleMemberDetails
    {
        [JsonProperty("@BuyingUnitInternalKey")]
        public string? BuyingUnitInternalKey { get; set;}
        [JsonProperty("@HouseHoldExternalId")]
        public string? HouseHoldExternalId { get; set; }
        [JsonProperty("@MemberInternalKey")]
        public string? MemberInternalKey { get; set; }
        [JsonProperty("@MemberExternalId")]
        public string? MemberExternalId { get; set; }
        [JsonProperty("@IsMainMember")]
        public string? IsMainMember { get; set; }
        [JsonProperty("@MemberStatus")]
        public string? MemberStatus { get; set; }
        [JsonProperty("@CardStatus")]
        public string? CardStatus { get; set; }
        [JsonProperty("@RedemptionPrivileges")]
        public string? RedemptionPrivileges { get; set; }

    }
}
