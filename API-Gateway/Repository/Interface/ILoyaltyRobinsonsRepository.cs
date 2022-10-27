using API_Gateway.data.Entites;
using API_Gateway.Models.Response;

namespace API_Gateway.Repository.Interface
{
    public interface ILoyaltyRobinsonsRepository
    {
        CrmMember? GetMember(string mobileNumber, DateTime? dateOfBirth);
        List<Member> GetMembers(string mobileNumber, DateTime? dateOfBirth);
        List<Member> GetMembers(string mobileNumber, string queryValue);
        List<Member> GetMembersUsingLastname(string mobileNumber, string queryValue);
        Task<List<Member>> GetMembersUsingMobileNumber(string mobileNumber);
        Task<List<Member>> GetMemberUsingLastname(string lastName);
        Task<List<Member>> GetMemberUsingEmail(string email);
        Task<List<Member>> GetMembersUsingMobileNumberExact(string mobileNumber);
        Task<List<Member>> GetMembersUsingMobileNumberLookup(string mobileNumber);
        Task InsertCrmMemberLookup(string cardNumber, string mobileNumber);
    }
}
