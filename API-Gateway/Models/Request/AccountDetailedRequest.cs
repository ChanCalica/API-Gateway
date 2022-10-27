using Newtonsoft.Json;

namespace API_Gateway.Models.Request
{
    public class AccountDetailedRequest
    {

        public FilteredAccountDetailedTransactions? FilteredAccountDetailedTransactions { get; set; }

        public AccountDetailedRequest()
        {
            
            FilteredAccountDetailedTransactions = new FilteredAccountDetailedTransactions();
        }


    }

    public class FilteredAccountDetailedTransactions
    {
        public SearchCriteria? SearchCriteria { get; set; }

        public member? Member { get; set; }

        public FilteredAccountDetailedTransactions()
        {

            SearchCriteria = new SearchCriteria();
            Member = new member();
        }

    }

    public class SearchCriteria
    {
        public TransactionDates? TransactionDates { get; set; }

        //public AccountActivityType? AccountActivityType { get; set; }

        public SearchCriteria()
        {

            TransactionDates = new TransactionDates();
            //AccountActivityType = new AccountActivityType();
        }
    }

    public class TransactionDates
    {
        [JsonProperty("@StartDate")]
        public string? StartDate { get; set; }
        [JsonProperty("@EndDate")]
        public string? EndDate { get; set; }

    }

    //public class AccountActivityType
    //{
    //    [JsonProperty("@Type")]
    //    public string? Type { get; set; }

    //}

    public class member
    {
        [JsonProperty("@ClubCardId")]
        public string? ClubCardId { get; set; }

        public Account? Account { get; set; }

        public member()
        {

            Account = new Account();
        }

    }

    public class Account
    {
        [JsonProperty("@Id")]
        public int? Id { get; set; }

    }
}
