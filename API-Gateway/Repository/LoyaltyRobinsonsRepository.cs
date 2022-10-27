using API_Gateway.Common.Data;
using API_Gateway.data.Entites;
using API_Gateway.Models.Response;
using API_Gateway.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using Microsoft.Data.SqlClient;

namespace API_Gateway.Repository
{
    public class LoyaltyRobinsonsRepository : ILoyaltyRobinsonsRepository
    {
        private readonly Loyalty_Robinsons_PreProdContext _context;

        public LoyaltyRobinsonsRepository(Loyalty_Robinsons_PreProdContext context)
        {
            _context = context;
        }

        public CrmMember? GetMember(string mobileNumber, DateTime? dateOfBirth)
        {
            var entityMember = _context.CrmMembers.FirstOrDefault(m => m.BirthDate == dateOfBirth && m.MobilePhoneNumber.Equals(mobileNumber));

            return entityMember;
        }

        public List<Member> GetMembers(string mobileNumber, DateTime? dateOfBirth)
        {
            var entityMembers = _context.CrmMembers.Where(m => m.BirthDate == dateOfBirth && m.MobilePhoneNumber.StartsWith("9").Equals(mobileNumber)).Select(o => new Member { FirstName = o.FirstName, LastName = o.LastName, MemberInternalKey = o.MemberInternalKey, ExternalMemberKey = o.ExternalMemberKey, BirthDate = o.BirthDate, UpdatedDate = o.UpdatedDate}).Distinct().ToList();

            return entityMembers;
        }

        public List<Member> GetMembers(string mobileNumber, string queryValue)
        {
            DateTime dateOfBirth = new DateTime();

            DateTime.TryParse(queryValue, out dateOfBirth);

            var entityMembers = _context.CrmMembers.Where(m => m.BirthDate == dateOfBirth && m.MobilePhoneNumber.Equals(mobileNumber)).Select(o => new Member { FirstName = o.FirstName, LastName = o.LastName, MemberInternalKey = o.MemberInternalKey, ExternalMemberKey = o.ExternalMemberKey, BirthDate = o.BirthDate, UpdatedDate = o.UpdatedDate, EmailAddress = o.EmailAddress }).Distinct().ToList();

            return entityMembers;
        }

        public List<Member> GetMembersUsingLastname(string mobileNumber, string queryValue)
        {
            try
            {
                var entityMembers = _context.CrmMembers.Where(m => m.LastName.Equals(queryValue) && m.MobilePhoneNumber.Equals(mobileNumber)).Select(o => new Member { FirstName = o.FirstName, LastName = o.LastName, MemberInternalKey = o.MemberInternalKey, ExternalMemberKey = o.ExternalMemberKey, BirthDate = o.BirthDate, UpdatedDate = o.UpdatedDate, EmailAddress = o.EmailAddress }).Distinct().ToList();

                return entityMembers;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    throw new ErrorException(HttpStatusCode.RequestTimeout, ex.Message);
                }

                throw ex;
            }
        }

        public async Task<List<Member>> GetMemberUsingLastname(string lastName)
        {
            try
            {
                var entityMembers = await _context.CrmMembers.Where(m => m.LastName.Equals(lastName)).Select(o => new Member { FirstName = o.FirstName, LastName = o.LastName, MemberInternalKey = o.MemberInternalKey, ExternalMemberKey = o.ExternalMemberKey, BirthDate = o.BirthDate, UpdatedDate = o.UpdatedDate, EmailAddress = o.EmailAddress }).ToListAsync();

                return entityMembers;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    throw new ErrorException(HttpStatusCode.RequestTimeout, ex.Message);
                }

                throw ex;
            }
        }

        public async Task<List<Member>> GetMemberUsingEmail(string email)
        {
            try
            {
                var entityMembers = await _context.CrmMembers.Where(m => m.EmailAddress.Equals(email)).Select(o => new Member { FirstName = o.FirstName, LastName = o.LastName, MemberInternalKey = o.MemberInternalKey, ExternalMemberKey = o.ExternalMemberKey, BirthDate = o.BirthDate, UpdatedDate = o.UpdatedDate, EmailAddress = o.EmailAddress }).ToListAsync();

                return entityMembers;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    throw new ErrorException(HttpStatusCode.RequestTimeout, ex.Message);
                }

                throw ex;
            }
        }

        public async Task<List<Member>> GetMembersUsingMobileNumber(string mobileNumber)
        {
            try
            {
                var entityMembers = await _context.CrmMembers.Where(m => m.MobilePhoneNumber.Contains(mobileNumber)).Select(s => new Member { FirstName = s.FirstName, LastName = s.LastName, MemberInternalKey = s.MemberInternalKey, ExternalMemberKey = s.ExternalMemberKey, BirthDate = s.BirthDate, UpdatedDate = s.UpdatedDate, EmailAddress = s.EmailAddress }).ToListAsync();

                return entityMembers;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    throw new ErrorException(HttpStatusCode.RequestTimeout, ex.Message);
                }

                throw ex;
            }
        }

        public async Task<List<Member>> GetMembersUsingMobileNumberExact(string mobileNumber)
        {
            try
            {
                var entityMembers = await _context.CrmMembers.Where(m => m.MobilePhoneNumber.Equals(mobileNumber)).Select(s => new Member { FirstName = s.FirstName, LastName = s.LastName, MemberInternalKey = s.MemberInternalKey, ExternalMemberKey = s.ExternalMemberKey, BirthDate = s.BirthDate, UpdatedDate = s.UpdatedDate, EmailAddress = s.EmailAddress }).ToListAsync();

                return entityMembers;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    throw new ErrorException(HttpStatusCode.RequestTimeout, ex.Message);
                }

                throw ex;
            }
        }

        public async Task<List<Member>> GetMembersUsingMobileNumberLookup(string mobileNumber)
        {
            try
            {
                var entityMembers = await _context.CrmMemberLookups.Where(m => m.MobilePhoneNumber.Equals(mobileNumber))
                                .Select(s => new Member { ExternalMemberKey = s.ExternalMemberKey })
                                .ToListAsync();

                return entityMembers;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    throw new ErrorException(HttpStatusCode.RequestTimeout, ex.Message);
                }

                throw ex;
            }
        }

        public async Task InsertCrmMemberLookup(string cardNumber, string mobileNumber)
        {
            try
            {
                //static values for memberInterKey, buyingUnitInternalKey, and recordVersion
                int? memberInternalKey = 10001;
                int? buyingUnitInternalKey = 10001;
                int? recordVersion = 0;
                string? mobilePhoneNumber = mobileNumber;
                string? externalMemberKey = cardNumber;

                var query = "INSERT INTO [dbo].[CRM_MemberLookup] " +
                    "([MemberInternalKey] ,[BuyingUnitInternalKey] ,[RecordVersion] ,[MobilePhoneNumber] ,[ExternalMemberKey]) " +
                    "VALUES " +
                    "(@MemberInternalKey, @BuyingUnitInternalKey, @RecordVersion, @MobilePhoneNumber, @ExternalMemberKey)";

                var response = await _context.Database.ExecuteSqlRawAsync(query,
                    new SqlParameter("@MemberInternalKey", memberInternalKey),
                    new SqlParameter("@BuyingUnitInternalKey", buyingUnitInternalKey),
                    new SqlParameter("@RecordVersion", recordVersion),
                    new SqlParameter("@MobilePhoneNumber", mobilePhoneNumber),
                    new SqlParameter("@ExternalMemberKey", externalMemberKey));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Execution Timeout Expired"))
                {
                    throw new ErrorException(HttpStatusCode.RequestTimeout, ex.Message);
                }

                throw ex;
            }
        }
    }
}
