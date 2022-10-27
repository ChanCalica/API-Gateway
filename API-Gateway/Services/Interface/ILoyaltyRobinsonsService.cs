using API_Gateway.Models.Request;
using API_Gateway.Models.Response;
using HQModels = LoyaltyOnlineWSUtilities.Models;
using OnlineMessagesService;

namespace API_Gateway.Services.Interface
{
    public interface ILoyaltyRobinsonsService
    {
        Member GetMember(MemberRequest request);
        List<Member> GetMembers(MemberRequest request);
        Task<MemberInfoResponse> GetMemberInfo(MemberRequest request);
        Task<MemberInfoResponse> CheckMember(CheckMemberRequest request, string tier);
        Task<CheckMemberInfoResponse> CheckMemberVersionTwo(CheckMemberRequest request);
        Task<MemberInfoResponse> GetMemberInfoUsingCardNumber(string cardNumber, string tier);
        Task<CreateResponse> CreateDemographics(DemographicRequest request);
        Task UpdateDemographics(DemographicRequest request, string cardNumber, string tier);
        Task<GetPointsResponse> GetPointsUsingCardNumber(string cardNumber);
        Task<RequestDataResponseBody> AdjustPoints(AdjustPointsRequest request);
        Task<RequestDataResponseBody> BatchPoints(BatchPointsRequest request, string retailerId);
        Task<CheckMemberInfoResponse> CheckMemberUsingQuery(CheckMemberRequest request);
        Task<HQModels.TransactionModel> GetMemberFilteredTransactions(TransactionsRequest FilteredDate, string cardId);
        Task<HQModels.AccountTransactionModel> GetMemberAccountTransactions(AccountDetailedRequest request, int limit);
        Task<CheckMemberInfoResponse> CheckMemberVersionThree(CheckMemberRequest request);
        Task<HQModels.MemberModel> CardValidate(string cardId);


    }
}
