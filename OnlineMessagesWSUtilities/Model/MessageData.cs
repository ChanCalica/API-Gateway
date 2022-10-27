using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMessagesWSUtilities.Model
{
    public class MessageData
    {
        public LoyaltyUploadInterface LoyaltyUploadInterface { get; set; }

        public MessageData()
        {
            LoyaltyUploadInterface = new LoyaltyUploadInterface();
        }
    }

    public class LoyaltyUploadInterface
    {
        public List<Root> Root { get; set; }

        public LoyaltyUploadInterface()
        {
            Root = new List<Root>();
        }
    }

    public class Root
    {
        public Customer Customer { get; set; }

        public Root()
        {
            Customer = new Customer();
        }
    }

    public class Customer
    {
        [JsonProperty("@MsgType")]
        public string? MsgType { get; set; }
        [JsonProperty("@LPEVer")]
        public string? LPEVer { get; set; }
        [JsonProperty("@RetailerId")]
        public string? RetailerId { get; set; }
        [JsonProperty("@StoreID")]
        public string? StoreID { get; set; }
        [JsonProperty("@StartDateTime")]
        public DateTime? StartDateTime { get; set; }
        [JsonProperty("@PosID")]
        public string? PosID { get; set; }
        [JsonProperty("@CashierID")]
        public string? CashierID { get; set; }
        [JsonProperty("@HomeStore")]
        public string? HomeStore { get; set; }
        [JsonProperty("@TransID")]
        public string? TransID { get; set; }
        [JsonProperty("@TicketIdentifier")]
        public string? TicketIdentifier { get; set; }
        [JsonProperty("@BusinessDate")]
        public string? BusinessDate { get; set; }
        [JsonProperty("@TicketTotal")]
        public string? TicketTotal { get; set; }
        public LoyaltyInfo LoyaltyInfo { get; set; }

        public Customer()
        {
            LoyaltyInfo = new LoyaltyInfo();
        }
    }

    public class LoyaltyInfo
    {
        [JsonProperty("@ServerDate")]
        public DateTime? ServerDate { get; set; }
        [JsonProperty("@CardID")]
        public string? CardID { get; set; }
        public Accounts Accounts { get; set; }

        public LoyaltyInfo()
        {
            Accounts = new Accounts();
        }
    }
    
    public class Accounts
    {
        public Acc Acc { get; set; }

        public Accounts()
        {
            Acc = new Acc();
        }
    }

    public class Acc
    {
        [JsonProperty("@ID")]
        public string? ID { get; set; }
        [JsonProperty("@EarnValue")]
        public string? EarnValue { get; set; }
        [JsonProperty("@RdmValue")]
        public string? RdmValue { get; set; }
        [JsonProperty("@ReasonCode")]
        public string? ReasonCode { get; set; }
    }
}
