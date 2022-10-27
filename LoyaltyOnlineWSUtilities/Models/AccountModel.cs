using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyOnlineWSUtilities.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Account
    {
        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("@EarnValue")]
        public string EarnValue { get; set; }

        [JsonProperty("@RedeemValue")]
        public string RedeemValue { get; set; }

        [JsonProperty("@Balance")]
        public string Balance { get; set; }

        [JsonProperty("@LastUpdate")]
        public string LastUpdate { get; set; }

        [JsonProperty("@HousekeepingBalance")]
        public string HousekeepingBalance { get; set; }

        [JsonProperty("@HousekeepingTotalAccumulated")]
        public string HousekeepingTotalAccumulated { get; set; }

        [JsonProperty("@HousekeepingTotalRedeemed")]
        public string HousekeepingTotalRedeemed { get; set; }

        [JsonProperty("@AccountName")]
        public string AccountName { get; set; }
    }

    public class Accounts
    {
        public Account Account { get; set; }
    }

    public class AccountModel
    {
        public Accounts Accounts { get; set; }
    }


}
