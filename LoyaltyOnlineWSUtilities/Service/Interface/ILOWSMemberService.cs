using LoyaltyOnlineWSMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyOnlineWSUtilities.Service.Interface
{
    public interface ILOWSMemberService
    {
        Task<GetDemographicResponse> GetDemographics(string cardNumber, string sessionKey);
        Task<GetHouseHoldAccountsActivityResponse> GetHouseholdAccountActivity(string cardNumber, string sessionKey);
        Task<Status> SaveDemographics(string cardNumber, string household, string sessionKey);
        Task<GetHouseHoldFilteredTransactionsResponse> GetHouseHoldFilteredTransactions(string cardNumber, string sessionKey, string transactionXMLString, string requestDate, string limit);
        Task<GetFilteredMemberAccountDetailedTransactionsResponse> GetFilteredMemberAccountDetailedTransactions(string account, string sessionKey);
        Task<GetHouseHoldMembersDemographicResponse> GetHouseholdMembers(string cardId, string sessionKey);
        Task<CardValidateResponse> CardValidate(string cardId, string sessionKey);
    }
}
