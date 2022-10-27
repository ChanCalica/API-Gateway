using API_Gateway.Common.Data;
using API_Gateway.Common.Logics.Interface;
using API_Gateway.Models.Request;
using API_Gateway.Models.Response;
using API_Gateway.Repository.Interface;
using API_Gateway.Services.Interface;
using API_Gateway.Utilities;
using HQModels = LoyaltyOnlineWSUtilities.Models;
using LoyaltyOnlineWSUtilities.Service.Interface;
using OnlineMessagesService;
using OnlineMessagesWSUtilities.Service.Interface;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using API_Gateway.data.Entites;
using OktaUtilities.Service.Interface;
using OktaApiUtilities.Models;
using System.Runtime.Caching;
using API_Gateway_FunctionApp.Service.Interface;

namespace API_Gateway.Services
{
    public class LoyaltyRobinsonsService : ILoyaltyRobinsonsService
    {
        private readonly ILoyaltyRobinsonsRepository _loyaltyRepository;
        private readonly ILOWSLoginService _loginService;
        private readonly ILOWSMemberService _memberService;
        private readonly IOnlineMWService _onlineMessageService;
        private readonly IFileManagerLogic _managerLogic;
        private readonly IDistributedCache _cache;
        private readonly IOktaClientService _oktaClientService;
        private readonly IFunctionAppService _functionAppService;
        private IConfiguration _Configuration { get; }
        public LoyaltyRobinsonsService(ILoyaltyRobinsonsRepository loyaltyRepository,
                                       ILOWSLoginService loginService,
                                       ILOWSMemberService memberService,
                                       IOnlineMWService onlineMessageService,
                                       IFileManagerLogic managerLogic,
                                       IDistributedCache cache,
                                       IConfiguration configuration,
                                       IOktaClientService oktaClientService,
                                       IFunctionAppService functionAppService)
        {
            _loyaltyRepository = loyaltyRepository;
            _loginService = loginService;
            _memberService = memberService;
            _onlineMessageService = onlineMessageService;
            _managerLogic = managerLogic;
            _cache = cache;
            _Configuration = configuration;
            _oktaClientService = oktaClientService;
            _functionAppService = functionAppService;
        }

        public Member GetMember(MemberRequest request)
        {
            var member = new Member();

            var entityMember = _loyaltyRepository.GetMember(request.mobileNumber, request.birthDate);

            if (entityMember != null)
            {
                member.MemberInternalKey = entityMember.MemberInternalKey;

                member.FirstName = entityMember.FirstName;
                member.LastName = entityMember.LastName;
                member.BirthDate = entityMember.BirthDate;
                member.ExternalMemberKey = entityMember.ExternalMemberKey;
            }

            return member;
        }

        public List<Member> GetMembers(MemberRequest request)
        {
            var members = new List<Member>();

            var entityMembers = _loyaltyRepository.GetMembers(request.mobileNumber, request.birthDate);

            if (entityMembers != null && entityMembers.Count > 0)
            {
                members.AddRange(entityMembers);
            }

            return members;
        }

        public async Task<MemberInfoResponse> GetMemberInfo(MemberRequest request)
        {
            try
            {
                if (!request.Query.ToLower().Equals("dob"))
                {
                    throw new ErrorException(HttpStatusCode.BadRequest);
                }

                var response = new MemberInfoResponse();
                var member = new Member();

                var entityMembers = _loyaltyRepository.GetMembers(request.mobileNumber, request.QueryValue);

                if (entityMembers == null || entityMembers.Count == 0)
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                var cardNumber = string.Empty;

                if (entityMembers != null && entityMembers.Count > 0)
                {
                    member = entityMembers.FirstOrDefault();
                    cardNumber = member.ExternalMemberKey;

                }

                if (!string.IsNullOrEmpty(cardNumber))
                {
                    var cachedSessionKey = await GetSessionKey();

                    var demographicResponse = await _memberService.GetDemographics(cardNumber, cachedSessionKey);
                    var householdActivityResponse = await _memberService.GetHouseholdAccountActivity(cardNumber, cachedSessionKey);

                    if (string.IsNullOrEmpty(demographicResponse.out_HouseHold) || string.IsNullOrEmpty(householdActivityResponse.out_Accounts))
                    {
                        throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                    }

                    response = MemberTranslator.TranslateMember(cardNumber, demographicResponse.out_HouseHold, householdActivityResponse.out_Accounts, null);
                }
                else
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MemberInfoResponse> CheckMember(CheckMemberRequest request, string? tier)
        {
            try
            {
                List<Member> entityMembers = new List<Member>();
                MemberInfoResponse response = new MemberInfoResponse();
                var member = new Member();
                string resultSet = string.Empty;
                var resultAttribute = string.Empty;

                entityMembers = _loyaltyRepository.GetMembers(request.MobileNumber, request.BirthDate);

                if (entityMembers == null || entityMembers.Count == 0)
                {
                    if (!string.IsNullOrEmpty(request.LastName))
                    {
                        entityMembers = _loyaltyRepository.GetMembersUsingLastname(request.MobileNumber, request.LastName);

                        if (entityMembers == null || entityMembers.Count == 0)
                        {
                            throw new ErrorException("ResultSet: Not Found");
                        }
                        else
                        {
                            throw new ErrorException("ResultSet: Discrepancy");
                        }
                    }
                    else
                    {
                        throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                    }
                }

                string cardNumber = string.Empty;

                if (entityMembers.Count > 0)
                {
                    entityMembers.OrderByDescending(x => x.UpdatedDate);

                    var sessionKey = await _loginService.Login();

                    foreach (var entityMember in entityMembers)
                    {
                        var members = entityMembers.Select(cardId => new { Count = cardId.ExternalMemberKey }).Distinct().Count();
                        if (members > 1)
                        {
                            cardNumber = entityMember.ExternalMemberKey;
                            if (!string.IsNullOrEmpty(cardNumber))
                            {

                                var demographicResponse = await _memberService.GetDemographics(cardNumber, sessionKey);
                                var householdActivityResponse = await _memberService.GetHouseholdAccountActivity(cardNumber, sessionKey);

                                if (string.IsNullOrEmpty(demographicResponse.out_HouseHold) || string.IsNullOrEmpty(householdActivityResponse.out_Accounts))
                                {
                                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                                }

                                var household = MemberTranslator.XMLtoHouseholdObject(demographicResponse.out_HouseHold);

                                var status = household.HouseHold.Members.Member.MemberStatus;

                                response = MemberTranslator.TranslateMember(cardNumber, demographicResponse.out_HouseHold, householdActivityResponse.out_Accounts, request.Tier);

                                if (!string.IsNullOrEmpty(status) && status != "0")
                                {
                                    var attributes = household.HouseHold.Members.Member.MemberAttributes.Attribute;
                                    foreach (var attribute in attributes)
                                    {
                                        int valueholder = 0;
                                        if (attribute.Id.Equals("10138"))
                                        {
                                            resultAttribute = attribute.Value;
                                            break;
                                        }
                                        else if (attribute.Id.Equals("10021"))
                                        {
                                            int newvalue = 0;
                                            int.TryParse(attribute.Value, out newvalue);
                                            if (newvalue > valueholder)
                                            {
                                                valueholder = newvalue;
                                                resultAttribute = valueholder.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            resultSet = "Match";
                        }
                    }
                }
                cardNumber = entityMembers.FirstOrDefault().ExternalMemberKey;

                if (string.IsNullOrEmpty(cardNumber))
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                response = await GetMemberInfoUsingCardNumber(cardNumber, tier);

                //response.ResultSet = resultSet;
                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CheckMemberInfoResponse> CheckMemberUsingQuery(CheckMemberRequest request)
        {
            try
            {
                List<Member> entityMembers = new List<Member>();
                CheckMemberInfoResponse response = new CheckMemberInfoResponse();
                MemberValidator.ValidateFilterRequest(request);

                var users = await _oktaClientService.SearchUser(null, request.FirstName, request.LastName, request.Email);

                if (users != null && users.Count > 0)
                {
                    users = await FilterMembers(users, request);
                }

                if (users == null || users.Count == 0)
                {
                    if (!string.IsNullOrEmpty(request.Email))
                    {
                        entityMembers = await _loyaltyRepository.GetMemberUsingEmail(request.Email);

                        if (entityMembers == null || entityMembers.Count == 0)
                        {
                            throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                        }
                    }
                    else if (!string.IsNullOrEmpty(request.LastName))
                    {
                        entityMembers = await _loyaltyRepository.GetMemberUsingLastname(request.LastName);

                        if (entityMembers == null || entityMembers.Count == 0)
                        {
                            throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                        }
                    }

                    string sessionKey = await _loginService.Login();

                    response = await FilterDBMembers(request, entityMembers, sessionKey);
                }
                else
                {
                    string sessionKey = await _loginService.Login();
                    response = await OktaToCheckMemberInfo(request, users, sessionKey);
                }

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CheckMemberInfoResponse> CheckMemberVersionTwo(CheckMemberRequest request)
        {
            try
            {
                MemberValidator.ValidateMobileNumber(request.MobileNumber);

                request.MobileNumber = MemberTranslator.SanitizeMobileNumber(request.MobileNumber);
                List<Member> entityMembers = new List<Member>();
                CheckMemberInfoResponse response = new CheckMemberInfoResponse();

                entityMembers = await _loyaltyRepository.GetMembersUsingMobileNumberExact(request.MobileNumber);

                if (entityMembers == null || entityMembers.Count == 0)
                {
                    request.MobileNumber = MemberTranslator.SanitizeMobileNumber(request.MobileNumber);
                    entityMembers = await _loyaltyRepository.GetMembersUsingMobileNumber(request.MobileNumber);
                }

                if (entityMembers == null || entityMembers.Count == 0)
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                string sessionKey = await _loginService.Login();

                response = await FilterDBMembers(request, entityMembers, sessionKey);

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CheckMemberInfoResponse> CheckMemberVersionThree(CheckMemberRequest request)
        {
            try
            {
                MemberValidator.ValidateMobileNumber(request.MobileNumber);
                List<Member> entityMembers = new List<Member>();
                CheckMemberInfoResponse response = new CheckMemberInfoResponse();

                string sanitizedMobileNumber = MemberTranslator.SanitizeMobileNumberAddPrefix(request.MobileNumber);
                var users = await _oktaClientService.SearchUser(sanitizedMobileNumber);

                if (users != null && users.Count > 0)
                {
                    users = await FilterMembers(users, request);

                    if (users.Count > 0)
                    {
                        string sessionKey = await _loginService.Login();
                        response = await OktaToCheckMemberInfo(request, users, sessionKey);
                    }
                }

                if (response.Members.Count <= 0)
                {
                    entityMembers = await _loyaltyRepository.GetMembersUsingMobileNumberExact(request.MobileNumber);

                    if (entityMembers == null || entityMembers.Count == 0)
                    {
                        request.MobileNumber = MemberTranslator.SanitizeMobileNumber(request.MobileNumber);
                        entityMembers = await _loyaltyRepository.GetMembersUsingMobileNumberLookup(request.MobileNumber);
                    }

                    if (entityMembers == null || entityMembers.Count == 0)
                    {
                        throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                    }

                    string sessionKey = await _loginService.Login();

                    response = await FilterDBMembers(request, entityMembers, sessionKey);
                }

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MemberInfoResponse> GetMemberInfoUsingCardNumber(string cardNumber, string? tier)
        {
            try
            {
                MemberInfoResponse response = new MemberInfoResponse();
                string resultSet = string.Empty;

                string sessionKey = await _loginService.Login();

                var demographicResponse = await _memberService.GetDemographics(cardNumber, sessionKey);
                var householdActivityResponse = await _memberService.GetHouseholdAccountActivity(cardNumber, sessionKey);

                if (string.IsNullOrEmpty(demographicResponse.out_HouseHold) || string.IsNullOrEmpty(householdActivityResponse.out_Accounts))
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }
                var household = MemberTranslator.XMLtoHouseholdObject(demographicResponse.out_HouseHold);

                if (household.HouseHold.Members.Member.Cards.Card.CardStatus != "1")
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                response = MemberTranslator.TranslateMember(cardNumber, demographicResponse.out_HouseHold, householdActivityResponse.out_Accounts, tier);

                resultSet = "Match";

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetPointsResponse> GetPointsUsingCardNumber(string cardNumber)
        {
            try
            {
                GetPointsResponse response = new GetPointsResponse();

                var cachedSessionKey = await GetCacheSessionKey();

                var householdActivityResponse = await _memberService.GetHouseholdAccountActivity(cardNumber, cachedSessionKey);

                if (string.IsNullOrEmpty(householdActivityResponse.out_Accounts))
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                response = MemberTranslator.MemberPoints(cardNumber, householdActivityResponse.out_Accounts);

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CreateResponse> CreateDemographics(DemographicRequest request)
        {
            bool IsRequestCardIdAuto = false;
            string cardNumber = string.Empty;
            try
            {
                CreateResponse result = new CreateResponse();
                MemberValidator.CreateMemberRequestValidator(request);
                MemberValidator.ValidateMobilePhoneNumber(request);

                if (request.Member.CardId.ToLower().Equals("auto"))
                {
                    IsRequestCardIdAuto = true;

                    if (request.Member != null && request.Member.MemberAttribute != null)
                    {
                        cardNumber = await _functionAppService.GetCardId(request.Member.MemberAttribute.Id);

                        if (string.IsNullOrEmpty(cardNumber))
                        {
                            throw new ErrorException(HttpStatusCode.NotFound);
                        }

                        request.Member.CardId = cardNumber;
                    }
                    else
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    cardNumber = request.Member.CardId;
                }

                result = MemberTranslator.CreatedMember(request);

                var household = MemberTranslator.CreateHouseholdXMLString(request);

                string sessionKey = await _loginService.Login();
                var response = await _memberService.SaveDemographics(cardNumber, household, sessionKey);

                if (response.Status1 == LoyaltyOnlineWSMember.ReMAEStatusType.Ok)
                {
                    string mobileNumber = request.Member.MobilePhoneNumber.Substring(request.Member.MobilePhoneNumber.Length - 10);
                    await _loyaltyRepository.InsertCrmMemberLookup(cardNumber, mobileNumber);
                }

                return result;
            }
            catch (ErrorException ex)
            {
                if (IsRequestCardIdAuto && !string.IsNullOrEmpty(cardNumber))
                {
                    await _functionAppService.ResetCardId(cardNumber);
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (IsRequestCardIdAuto && !string.IsNullOrEmpty(cardNumber))
                {
                    await _functionAppService.ResetCardId(cardNumber);
                }
                throw ex;
            }
        }

        public async Task UpdateDemographics(DemographicRequest request, string cardNumber, string? tier)
        {
            try
            {
                MemberValidator.UpdateMemberRequestValidator(request);
                MemberValidator.ValidateMobilePhoneNumber(request);
                //MemberValidator.DisallowPIIChange(request);

                var sessionKey = await _loginService.Login();
                var demographicResponse = await _memberService.GetDemographics(cardNumber, sessionKey);
                if (string.IsNullOrEmpty(demographicResponse.out_HouseHold))
                {
                    throw new ErrorException(HttpStatusCode.NotFound, demographicResponse.GetDemographicResult.Message, "HQ Error");
                }

                var householdXMLString = MemberTranslator.CreateHouseholdXMLString(demographicResponse.out_HouseHold, request);

                var response = await _memberService.SaveDemographics(cardNumber, householdXMLString, sessionKey);

                var oldUser = await _oktaClientService.SearchUserUsingGRCardNumber(cardNumber);

                if (oldUser != null && oldUser.Count > 0)
                {
                    var userRequest = OktaModelTranslator.CreateUserUpdateRequest(request, cardNumber, oldUser[0]);

                    var user = await _oktaClientService.UpdateUser(oldUser[0].Id, userRequest);
                }

                await SavePIIChanges(request, cardNumber, tier);
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RequestDataResponseBody> AdjustPoints(AdjustPointsRequest request)
        {
            try
            {
                MemberValidator.ValidateAdjustPointsRequest(request);
                RequestDataResponseBody response = new RequestDataResponseBody();

                MSG3RequestObjects msg3Objects = new MSG3RequestObjects();

                msg3Objects = MemberTranslator.CreateMSG3RequestObjects(request);

                if (msg3Objects.RequestObjects.Count > 0)
                {
                    foreach (var requestObject in msg3Objects.RequestObjects)
                    {
                        var result = await _onlineMessageService.RequestDataMessages(requestObject.MessageData, requestObject.Chain, requestObject.Branch);
                    }
                }

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RequestDataResponseBody> BatchPoints(BatchPointsRequest request, string retailId)
        {
            try
            {
                MemberValidator.ValidateBatchPointsRequest(request);
                RequestDataResponseBody response = new RequestDataResponseBody();

                MSG3RequestObjects msg3Objects = new MSG3RequestObjects();


                msg3Objects = MemberTranslator.CreateMSG3BatchRequestObjects(request, retailId);


                if (msg3Objects.RequestObjects.Count > 0)
                {
                    foreach (var requestObject in msg3Objects.RequestObjects)
                    {
                        var result = await _onlineMessageService.RequestDataMessages(requestObject.MessageData, requestObject.Chain, requestObject.Branch);
                    }
                }

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HQModels.TransactionModel> GetMemberFilteredTransactions(TransactionsRequest request, string cardId)
        {
            try
            {
                HQModels.TransactionModel response = new HQModels.TransactionModel();
              
                var sessionKey = await _loginService.Login();

                var transactionXMLString = MemberTranslator.FilteredTransactionsToXMLString(request);

                var requestDate = request.StartDate;

                var limit = request.MaxRows;

                var householdTransactionsResponse = await _memberService.GetHouseHoldFilteredTransactions(cardId, sessionKey, transactionXMLString, requestDate, limit);

                if (string.IsNullOrEmpty(householdTransactionsResponse.out_HouseHoldTransactions))
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");

                }

                response = MemberTranslator.MemberTransactions(householdTransactionsResponse.out_HouseHoldTransactions, cardId, limit);

                return response;


            }
            catch (ErrorException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<HQModels.AccountTransactionModel> GetMemberAccountTransactions(AccountDetailedRequest request, int limit)
        {
            try
            {
                HQModels.AccountTransactionModel response = new HQModels.AccountTransactionModel();

                var sessionKey = await _loginService.Login();

                var accountTransactionsXMLString = MemberTranslator.AccountTransactionsToXMLString(request);

                var accountTransactionsResponse = await _memberService.GetFilteredMemberAccountDetailedTransactions(accountTransactionsXMLString, sessionKey);


                if (string.IsNullOrEmpty(accountTransactionsResponse.out_HouseHoldTransactions))
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");

                }

                var requestDate = request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.StartDate;

                response = MemberTranslator.AccountTransactions(accountTransactionsResponse.out_HouseHoldTransactions, requestDate, limit);

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<HQModels.MemberModel> CardValidate(string cardId)
        {
            try
            {
                HQModels.MemberModel response = new HQModels.MemberModel();

                MemberValidator.CardValidate(cardId);

                var sessionKeyCached = await StoreCache();

                var singleMemberResponse = await _memberService.CardValidate(cardId, sessionKeyCached);

                if (string.IsNullOrEmpty(singleMemberResponse.out_Member))
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");

                }

                response = MemberTranslator.SingleMemberDetails(singleMemberResponse.out_Member);

                return response;
            }
            catch(ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Private Methods

        private async Task<string> StoreCache()
        {
            string sessionkey = string.Empty;

            ObjectCache cache = MemoryCache.Default;

            var isExists = cache.Contains("SessionKey");

            if (!isExists)
            {
                var sessionKey = await _loginService.Login();

                var cacheItem = new CacheItem("SessionKey", sessionKey);

                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
                };

                var result = cache.Add(cacheItem, cacheItemPolicy);

                if (result)
                {
                    var value = cache.Get("SessionKey");

                    sessionkey = value.ToString();
                }
            }
            else
            {
                var value = cache.Get("SessionKey");

                sessionkey = value.ToString();
            }

            return sessionkey;
        }
        private async Task<string> GetCacheSessionKey()
        {
            string sessionKey = string.Empty;

            ObjectCache cache = MemoryCache.Default;

            var isExists = cache.Contains("SessionKey");

            if (!isExists)
            {
                sessionKey = await _loginService.Login();

                var cacheSessionKey = new CacheItem("SessionKey", sessionKey);

                var timeoutValue = _Configuration.GetSection("HQ_SK").GetValue<string>("Timeout");

                double timeOut = Convert.ToDouble(timeoutValue);

                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(timeOut)
                };

                cache.Add(cacheSessionKey, cacheItemPolicy);

                var valueSessionKey = cache.Get("SessionKey");

                sessionKey = valueSessionKey.ToString();

            }
            else
            {
                var valueSessionKey = cache.Get("SessionKey");

                sessionKey = valueSessionKey.ToString();
            }

            return sessionKey;
        }
        private async Task<string> GetSessionKey()
        {
            string sessionKey = string.Empty;
            //_cache.Remove("Key");

            var redisSessionKey = _cache.GetString("Key");

            if (string.IsNullOrEmpty(redisSessionKey))
            {
                sessionKey = await RefreshSessionKey();
            }
            else
            {
                var cacheValues = redisSessionKey.Split("|");

                sessionKey = cacheValues[0];

                DateTime timestamp = DateTime.MinValue;

                DateTime.TryParse(cacheValues[1], out timestamp);

                if (timestamp != DateTime.MinValue)
                {
                    //var datetimeNow = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt"); //local
                    var datetimeNow = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss tt"); //azure
                   

                    DateTime timestampNow = DateTime.MinValue;

                    DateTime.TryParse(datetimeNow, out timestampNow);

                    var timeoutValue = _Configuration.GetSection("HQ_SK").GetValue<string>("Timeout");

                    double timeOut = Convert.ToDouble(timeoutValue);

                    if (timestampNow > timestamp.AddMinutes(timeOut))
                    {
                        sessionKey = await RefreshSessionKey();
                    }
                }

            }

            return sessionKey;

        }

        private async Task<string> RefreshSessionKey()
        {
            string sessionKey = await _loginService.Login();



            //var timestamp = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt"); //local
            var timestamp = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss tt"); //azure
            

            var cachedSessionKey = sessionKey + "|" + timestamp;

            _cache.SetString("Key", cachedSessionKey);

            return sessionKey;
        }

        private List<Member> FilterMembers(List<Member> entityMembers, CheckMemberRequest request)
        {
            var result = new List<Member>();

            if (!string.IsNullOrEmpty(request.LastName))
            {
                entityMembers = entityMembers.Where(o => request.LastName.ToUpper().Equals(o.LastName?.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                entityMembers = entityMembers.Where(o => request.Email.ToUpper().Equals(o.EmailAddress?.ToUpper()) || string.IsNullOrEmpty(o.EmailAddress)).ToList();
            }

            if (request.BirthDate != null && request.BirthDate != DateTime.MinValue)
            {
                entityMembers = entityMembers.Where(o => o.BirthDate == request.BirthDate).ToList();
            }

            result = entityMembers;

            return result;
        }

        private List<MemberInfoResponse> FilterMembersResponse(List<MemberInfoResponse> entityMembers, CheckMemberRequest request)
        {
            var result = new List<MemberInfoResponse>();

            if (!string.IsNullOrEmpty(request.LastName))
            {
                entityMembers = entityMembers.Where(o => request.LastName.ToUpper().Equals(o.Member.LastName?.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                entityMembers = entityMembers.Where(o => request.Email.ToUpper().Equals(o.Member.EmailAddress?.ToUpper())).ToList();
            }

            if (request.BirthDate != null && request.BirthDate != DateTime.MinValue)
            {
                entityMembers = entityMembers.Where(o => o.Member.BirthDate != null && o.Member.BirthDate == request.BirthDate).ToList();
            }

            result = entityMembers;

            return result;
        }

        private async Task<List<User>> FilterMembers(List<User> users, CheckMemberRequest request)
        {
            var result = new List<User>();

            if (!string.IsNullOrEmpty(request.LastName))
            {
                users = users.Where(o => request.LastName.ToUpper().Equals(o.Profile.LastName.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                users = users.Where(o => request.Email.ToUpper().Equals(o.Profile.Email.ToUpper())).ToList();
            }

            if (request.BirthDate != null && request.BirthDate != DateTime.MinValue)
            {
                var birthdate = BdayFormats(request.BirthDate);

                users = users.Where(o => o.Profile.DOB != null && (o.Profile.DOB.Equals(birthdate[0]) || o.Profile.DOB.Equals(birthdate[1]))).ToList();
            }

            result = users;

            return result;
        }

        private List<string> BdayFormats(DateTime? bday)
        {
            List<string> birthdates = new List<string>();

            if (bday != null)
            {
                var bdayString = bday.ToString();

                var bdaySubString = bdayString.Split(' ');

                var bdayDate = bdaySubString[0].Split('/');

                //Date Format
                //dd/MM/yyyy hh:mm:ss tt //local
                //MM/dd/yyyy hh:mm:ss tt //azure

                //removes leading 0 on Month value
                //bdayDate[1] local
                //bdayDate[0] azure
                var bdayMonthToInt = int.Parse(bdayDate[0]);
                var bdayMonthToString = bdayMonthToInt.ToString();

                //removes leading 0 on Day value
                //bdayDate[0] local
                //bdayDate[1] azure
                var bdayDayToInt = int.Parse(bdayDate[1]);
                var bdayDayToString = bdayDayToInt.ToString();

                // Date Format MM/DD/YYYY
                var bdaySlashFormat = bdayMonthToString + "/" + bdayDayToString + "/" + bdayDate[2];
                birthdates.Add(bdaySlashFormat);

                // Date Format YYYY-MM-DD
                var bdayHypenFormat = bdayDate[2] + "-" + bdayDate[1] + "-" + bdayDate[0];
                birthdates.Add(bdayHypenFormat);
            }

            return birthdates;
        }

        private List<CrmMemberDev> FilterMembers(List<CrmMemberDev> entityMembers, CheckMemberRequest request)
        {
            var result = new List<CrmMemberDev>();

            if (!string.IsNullOrEmpty(request.LastName))
            {
                entityMembers = entityMembers.Where(o => request.LastName.Equals(o.LastName)).ToList();
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                entityMembers = entityMembers.Where(o => request.Email.Equals(o.EmailAddress)).ToList();
            }

            if (request.BirthDate != null && request.BirthDate != DateTime.MinValue)
            {
                entityMembers = entityMembers.Where(o => o.BirthDate == request.BirthDate || o.BirthDate == null).ToList();
            }

            result = entityMembers;

            return result;
        }
        private List<Member> FilterMember(List<Member> entityMembers, CheckMemberRequest request)
        {
            var result = new List<Member>();

            if (request.BirthDate != null && request.BirthDate != DateTime.MinValue)
            {
                entityMembers = entityMembers.Where(o => o.BirthDate == request.BirthDate || o.BirthDate == null).ToList();
            }

            result = entityMembers;

            return result;
        }

        private bool CheckAttribute(List<LoyaltyOnlineWSUtilities.Models.Attribute> attributes)
        {
            bool isAttributeValid = false;
            if (attributes != null && attributes.Count > 0)
            {
                foreach (var attribute in attributes)
                {
                    int valueholder = 0;
                    if (attribute.Id.Equals("10138"))
                    {
                        isAttributeValid = true;
                        //resultAttribute = attribute.Value;
                        break;
                    }
                    else if (attribute.Id.Equals("10021"))
                    {
                        int newvalue = 0;
                        int.TryParse(attribute.Value, out newvalue);
                        if (newvalue > valueholder)
                        {
                            valueholder = newvalue;
                            isAttributeValid = true;
                            //resultAttribute = valueholder.ToString();
                        }
                    }
                }
            }

            return isAttributeValid;
        }

        private bool CheckAttribute(List<LoyaltyOnlineWSUtilities.Models.AttributeMemberDemographic> attributes)
        {
            bool isAttributeValid = false;
            if (attributes != null && attributes.Count > 0)
            {
                foreach (var attribute in attributes)
                {
                    int valueholder = 0;
                    if (attribute.Id.Equals("10138"))
                    {
                        isAttributeValid = true;
                        //resultAttribute = attribute.Value;
                        break;
                    }
                    else if (attribute.Id.Equals("10021"))
                    {
                        int newvalue = 0;
                        int.TryParse(attribute.Value, out newvalue);
                        if (newvalue > valueholder)
                        {
                            valueholder = newvalue;
                            isAttributeValid = true;
                            //resultAttribute = valueholder.ToString();
                        }
                    }
                }
            }

            return isAttributeValid;
        }

        private async Task<DemographicsAndAccounts> GetDemographicsAndAccounts(string cardNumber, string sessionKey)
        {
            var response = new DemographicsAndAccounts();
            var demographicResponse = await _memberService.GetDemographics(cardNumber, sessionKey);
            var householdActivityResponse = await _memberService.GetHouseholdAccountActivity(cardNumber, sessionKey);

            if (string.IsNullOrEmpty(demographicResponse.out_HouseHold) || string.IsNullOrEmpty(householdActivityResponse.out_Accounts))
            {
                throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
            }

            response.Household = demographicResponse.out_HouseHold;
            response.Accounts = householdActivityResponse.out_Accounts;

            return response;
        }

        private async Task<CheckMemberInfoResponse> FilterDBMembers(CheckMemberRequest request, List<Member> entityMembers, string sessionKey)
        {
            try
            {
                CheckMemberInfoResponse response = new CheckMemberInfoResponse();

                if (entityMembers.Count > 0)
                {
                    if (entityMembers.Count == 1)
                    {
                        DemographicsAndAccounts demographicAndAccounts = new DemographicsAndAccounts();
                        MemberInfoResponse translatedMember = new MemberInfoResponse();
                        response.ResultSet = "Match";

                        if (!string.IsNullOrEmpty(request.LastName))
                        {
                            if (entityMembers[0].LastName != null && !entityMembers[0].LastName.ToLower().Equals(request.LastName.ToLower()))
                            {
                                response.ResultSet = "No Match";
                            }
                        }

                        var cardNumber = entityMembers[0].ExternalMemberKey;

                        demographicAndAccounts = await GetHouseholdMembers(cardNumber, sessionKey);

                        if (string.IsNullOrEmpty(demographicAndAccounts.Household))
                        {
                            demographicAndAccounts = await GetDemographicsAndAccounts(cardNumber, sessionKey);

                            var householdModel = MemberTranslator.XMLtoHouseholdObject(demographicAndAccounts.Household);

                            if (!householdModel.HouseHold.Members.Member.Cards.Card.CardStatus.Equals("1"))
                            {
                                throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                            }

                            translatedMember = MemberTranslator.TranslateMember(cardNumber, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);

                            response.Members.Add(translatedMember);
                        }
                        else
                        {
                            var householdObject = MemberTranslator.XMLtoHouseholdMemberObject(demographicAndAccounts.Household);
                            var activeMember = MemberTranslator.GetActiveMemberDemographic(householdObject.HouseHold.Members);
                            var householdAccount = await _memberService.GetHouseholdAccountActivity(activeMember.Cards.Card.Id, sessionKey);

                            if (string.IsNullOrEmpty(householdAccount.out_Accounts) || !activeMember.Cards.Card.Id.Equals(cardNumber) || !activeMember.Cards.Card.CardStatus.Equals("1"))
                            {
                                throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                            }

                            demographicAndAccounts.Accounts = householdAccount.out_Accounts;

                            translatedMember = MemberTranslator.TranslateDemographicMember(cardNumber, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);

                            response.Members.Add(translatedMember);
                        }
                    }
                    else
                    {
                        foreach (var member in entityMembers)
                        {
                            MemberInfoResponse translatedMember = new MemberInfoResponse();
                            string status = string.Empty;
                            bool isAttributePriority = false;
                            var cardNumber = member.ExternalMemberKey;

                            var demographicAndAccounts = await GetHouseholdMembers(cardNumber, sessionKey);

                            if (string.IsNullOrEmpty(demographicAndAccounts.Household))
                            {
                                var demographicsData = await _memberService.GetDemographics(cardNumber, sessionKey);

                                if (string.IsNullOrEmpty(demographicsData.out_HouseHold))
                                {
                                    continue;
                                }

                                var accountData = await _memberService.GetHouseholdAccountActivity(cardNumber, sessionKey);

                                if (string.IsNullOrEmpty(accountData.out_Accounts))
                                {
                                    continue;
                                }

                                var memberData = MemberTranslator.XMLtoHouseholdObject(demographicsData.out_HouseHold);

                                if (!memberData.HouseHold.Members.Member.Cards.Card.CardStatus.Equals("1"))
                                {
                                    continue;
                                }

                                status = memberData.HouseHold.Members.Member.MemberStatus;
                                isAttributePriority = CheckAttribute(memberData.HouseHold.Members.Member.MemberAttributes.Attribute);
                                translatedMember = MemberTranslator.TranslateMember(cardNumber, demographicsData.out_HouseHold, accountData.out_Accounts, request.Tier);
                            }
                            else
                            {
                                var householdObject = MemberTranslator.XMLtoHouseholdMemberObject(demographicAndAccounts.Household);
                                var activeMember = MemberTranslator.GetActiveMemberDemographic(householdObject.HouseHold.Members);
                                var householdAccount = await _memberService.GetHouseholdAccountActivity(activeMember.Cards.Card.Id, sessionKey);

                                if (string.IsNullOrEmpty(householdAccount.out_Accounts) || !activeMember.Cards.Card.Id.Equals(cardNumber) || !activeMember.Cards.Card.CardStatus.Equals("1"))
                                {
                                    continue;
                                }

                                demographicAndAccounts.Accounts = householdAccount.out_Accounts;
                                status = activeMember.Cards.Card.CardStatus;
                                isAttributePriority = CheckAttribute(activeMember.MemberAttributes.Attribute);
                                translatedMember = MemberTranslator.TranslateDemographicMember(cardNumber, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);
                            }

                            if (!string.IsNullOrEmpty(status) && status != "0")
                            {
                                int index = 0;
                                bool replaceMember = false;
                                if (isAttributePriority)
                                {
                                    foreach (var newMember in response.Members)
                                    {
                                        if (newMember.CardId.Equals(cardNumber))
                                        {
                                            index = response.Members.IndexOf(newMember);
                                            replaceMember = true;
                                            break;
                                        }
                                        replaceMember = false;
                                    }
                                }
                                if (replaceMember)
                                {
                                    response.Members[index] = translatedMember;
                                }
                                else
                                {
                                    response.Members.Add(translatedMember);
                                }
                            }
                        }
                    }

                    if (response.Members.Count > 0)
                    {
                        response.Members = FilterMembersResponse(response.Members, request);

                        response.Members = FilterPriorityMembers(response.Members);

                        if (response.Members.Count > 1)
                        {
                            response.ResultSet = "Multiple";
                        }
                        else
                        {
                            response.ResultSet = "Match";
                        }
                    }

                    if (response.Members.Count == 0)
                    {
                        throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                    }
                }

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<DemographicsAndAccounts> GetMemberDemographicsAndAccounts(string cardNumber, string sessionKey)
        {
            var response = new DemographicsAndAccounts();
            var demographicResponse = await _memberService.GetHouseholdMembers(cardNumber, sessionKey);

            var householdActivityResponse = await _memberService.GetHouseholdAccountActivity(cardNumber, sessionKey);

            if (string.IsNullOrEmpty(demographicResponse.out_HouseHold) || string.IsNullOrEmpty(householdActivityResponse.out_Accounts))
            {
                throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
            }

            response.Household = demographicResponse.out_HouseHold;
            response.Accounts = householdActivityResponse.out_Accounts;

            return response;
        }

        private async Task<DemographicsAndAccounts> GetHouseholdMembers(string cardNumber, string sessionKey)
        {
            var response = new DemographicsAndAccounts();
            var demographicResponse = await _memberService.GetHouseholdMembers(cardNumber, sessionKey);

            response.Household = demographicResponse.out_HouseHold;

            return response;
        }

        private async Task SavePIIChanges(DemographicRequest request, string cardNumber, string tier)
        {

            var updateRequest = new DemographicUpdateRequest()
            {
                EmailAddress = request.Member.EmailAddress,
                FirstName = request.Member.FirstName,
                LastName = request.Member.LastName,
                MiddleInitial = request.Member.MiddleInitial,
                BirthDate = request.Member.BirthDate,
                MobilePhoneNumber = request.Member.MobilePhoneNumber,
                CardId = cardNumber


            };

            var result = JsonSerializer.Serialize(updateRequest);

            string fileName = DateTime.Now.ToString("yyyyMMddHHmm") + "-" + tier + "-" + cardNumber.Substring(12, 4) + ".txt";

            await _managerLogic.MemberUpdateUploadBlob(fileName, result);

        }

        private async Task<CheckMemberInfoResponse> OktaToCheckMemberInfo(CheckMemberRequest request, User user, string sessionKey)
        {
            try
            {
                CheckMemberInfoResponse response = new CheckMemberInfoResponse();
                DemographicsAndAccounts demographicAndAccounts = new DemographicsAndAccounts();
                MemberInfoResponse translatedMember = new MemberInfoResponse();

                response.ResultSet = "Match";

                if (!string.IsNullOrEmpty(request.LastName))
                {
                    if (!user.Profile.LastName.ToLower().Equals(request.LastName.ToLower()))
                    {
                        response.ResultSet = "No Match";
                    }
                }

                var cardNumber = user.Profile.GRCardNumPrim.ToString();

                demographicAndAccounts = await GetHouseholdMembers(cardNumber, sessionKey);

                if (string.IsNullOrEmpty(demographicAndAccounts.Household))
                {
                    demographicAndAccounts = await GetDemographicsAndAccounts(cardNumber, sessionKey);

                    translatedMember = MemberTranslator.TranslateMember(cardNumber, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);

                    response.Members.Add(translatedMember);
                }
                else
                {
                    var householdObject = MemberTranslator.XMLtoHouseholdMemberObject(demographicAndAccounts.Household);
                    var activeMember = MemberTranslator.GetActiveMemberDemographic(householdObject.HouseHold.Members);
                    var householdAccount = await _memberService.GetHouseholdAccountActivity(activeMember.Cards.Card.Id, sessionKey);

                    if (string.IsNullOrEmpty(householdAccount.out_Accounts) || !activeMember.Cards.Card.Id.Equals(cardNumber))
                    {
                        throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                    }

                    demographicAndAccounts.Accounts = householdAccount.out_Accounts;

                    translatedMember = MemberTranslator.TranslateDemographicMember(cardNumber, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);

                    response.Members.Add(translatedMember);
                }

                return response;
            }
            catch (ErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<CheckMemberInfoResponse> OktaToCheckMemberInfo(CheckMemberRequest request, List<User> users, string sessionKey)
        {
            CheckMemberInfoResponse response = new CheckMemberInfoResponse();

            if (users.Count == 1)
            {
                DemographicsAndAccounts demographicAndAccounts = new DemographicsAndAccounts();
                MemberInfoResponse translatedMember = new MemberInfoResponse();
                response.ResultSet = "Match";

                if (!string.IsNullOrEmpty(request.LastName))
                {
                    if (!users[0].Profile.LastName.ToLower().Equals(request.LastName.ToLower()))
                    {
                        response.ResultSet = "No Match";
                    }
                }

                var cardNumber = users[0].Profile.GRCardNumPrim.ToString();

                demographicAndAccounts = await GetHouseholdMembers(cardNumber, sessionKey);

                if (string.IsNullOrEmpty(demographicAndAccounts.Household))
                {
                    demographicAndAccounts = await GetDemographicsAndAccounts(cardNumber, sessionKey);

                    var householdModel = MemberTranslator.XMLtoHouseholdObject(demographicAndAccounts.Household);

                    if (!householdModel.HouseHold.Members.Member.Cards.Card.CardStatus.Equals("1"))
                    {
                        return response;
                    }

                    translatedMember = MemberTranslator.TranslateMember(cardNumber, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);

                    response.Members.Add(translatedMember);
                }
                else
                {
                    var householdObject = MemberTranslator.XMLtoHouseholdMemberObject(demographicAndAccounts.Household);
                  
                    foreach (var member in householdObject.HouseHold.Members.Member)
                    {
                        var cardId = member.MemberExternalId;

                        var activeMember = MemberTranslator.GetActiveMemberDemographicforOkta(householdObject.HouseHold.Members, cardId);

                        if (!string.IsNullOrEmpty(activeMember.MemberExternalId))
                        {
                            if (!activeMember.Cards.Card.CardStatus.Equals("1"))
                            {
                                continue;
                            }

                            var householdAccount = await _memberService.GetHouseholdAccountActivity(activeMember.Cards.Card.Id, sessionKey);

                            if (string.IsNullOrEmpty(householdAccount.out_Accounts))
                            {
                                continue;
                            }

                            demographicAndAccounts.Accounts = householdAccount.out_Accounts;

                            translatedMember = MemberTranslator.TranslateDemographicMemberforOkta(cardId, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);

                            response.Members.Add(translatedMember);

                        }
                    }
                }
            }
            else
            {
                foreach (var member in users)
                {
                    MemberInfoResponse translatedMember = new MemberInfoResponse();
                    string status = string.Empty;
                    bool isAttributePriority = false;
                    var cardNumber = member.Profile.GRCardNumPrim.ToString();

                    var demographicAndAccounts = await GetHouseholdMembers(cardNumber, sessionKey);

                    if (string.IsNullOrEmpty(demographicAndAccounts.Household))
                    {
                        var demographicsData = await _memberService.GetDemographics(cardNumber, sessionKey);

                        if (string.IsNullOrEmpty(demographicsData.out_HouseHold))
                        {
                            continue;
                        }

                        var accountData = await _memberService.GetHouseholdAccountActivity(cardNumber, sessionKey);

                        if (string.IsNullOrEmpty(accountData.out_Accounts))
                        {
                            continue;
                        }

                        var memberData = MemberTranslator.XMLtoHouseholdObject(demographicsData.out_HouseHold);

                        if (!memberData.HouseHold.Members.Member.Cards.Card.CardStatus.Equals("1"))
                        {
                            continue;
                        }

                        status = memberData.HouseHold.Members.Member.MemberStatus;
                        isAttributePriority = CheckAttribute(memberData.HouseHold.Members.Member.MemberAttributes.Attribute);
                        translatedMember = MemberTranslator.TranslateMember(cardNumber, demographicsData.out_HouseHold, accountData.out_Accounts, request.Tier);
                    }
                    else
                    {
                        var householdObject = MemberTranslator.XMLtoHouseholdMemberObject(demographicAndAccounts.Household);
                        var activeMember = MemberTranslator.GetActiveMemberDemographic(householdObject.HouseHold.Members);
                        var householdAccount = await _memberService.GetHouseholdAccountActivity(activeMember.Cards.Card.Id, sessionKey);

                        if (string.IsNullOrEmpty(householdAccount.out_Accounts) || !activeMember.Cards.Card.Id.Equals(cardNumber) || !activeMember.Cards.Card.CardStatus.Equals("1"))
                        {
                            continue;
                        }

                        demographicAndAccounts.Accounts = householdAccount.out_Accounts;
                        status = activeMember.Cards.Card.CardStatus;
                        isAttributePriority = CheckAttribute(activeMember.MemberAttributes.Attribute);
                        translatedMember = MemberTranslator.TranslateDemographicMember(cardNumber, demographicAndAccounts.Household, demographicAndAccounts.Accounts, request.Tier);
                    }

                    if (!string.IsNullOrEmpty(status) && status != "0")
                    {
                        int index = 0;
                        bool replaceMember = false;
                        if (isAttributePriority)
                        {
                            foreach (var newMember in response.Members)
                            {
                                if (newMember.CardId.Equals(cardNumber))
                                {
                                    index = response.Members.IndexOf(newMember);
                                    replaceMember = true;
                                    break;
                                }
                                replaceMember = false;
                            }
                        }
                        if (replaceMember)
                        {
                            response.Members[index] = translatedMember;
                        }
                        else
                        {
                            response.Members.Add(translatedMember);
                        }
                    }
                }
            }

            if (response.Members.Count > 0)
            {
                response.Members = FilterMembersResponse(response.Members, request);

                response.Members = FilterPriorityMembers(response.Members);

                if (response.Members.Count > 1)
                {
                    response.ResultSet = "Multiple";
                }
                else if (response.Members.Count == 1)
                {
                    response.ResultSet = "Match";
                }
            }

            return response;
        }

        private List<MemberInfoResponse> FilterPriorityMembers(List<MemberInfoResponse> members)
        {
            List<MemberInfoResponse> response = new List<MemberInfoResponse>();

            response.AddRange(FilterPrioritySeven(members));

            if (response.Count < 1)
            {
                response.AddRange(FilterPriorityTwo(members));
            }

            if (response.Count < 1)
            {
                response.AddRange(FilterPriorityFourFiveSix(members));
            }

            return response;
        }

        private List<MemberInfoResponse> FilterPrioritySeven(List<MemberInfoResponse> members)
        {
            List<MemberInfoResponse> response = new List<MemberInfoResponse>();

            var cardPrefixes = _Configuration.GetSection("Card_Prefix_Rank").GetValue<string>("Rank_Seven");

            var cardPrefixesArray = Array.Empty<string>();

            if (!string.IsNullOrEmpty(cardPrefixes))
            {
                cardPrefixesArray = cardPrefixes.Split(',');
            }

            if (cardPrefixesArray != null && cardPrefixesArray.Length > 0)
            {
                foreach (var member in members)
                {
                    foreach (var cardPrefix in cardPrefixesArray)
                    {
                        if (member.CardId.StartsWith(cardPrefix))
                        {
                            response.Add(member);
                            continue;
                        }
                    }
                }
            }

            return response;
        }

        private List<MemberInfoResponse> FilterPriorityTwo(List<MemberInfoResponse> members)
        {
            List<MemberInfoResponse> response = new List<MemberInfoResponse>();

            var cardPrefixes = _Configuration.GetSection("Card_Prefix_Rank").GetValue<string>("Rank_Two");

            var cardPrefixesArray = Array.Empty<string>();

            if (!string.IsNullOrEmpty(cardPrefixes))
            {
                cardPrefixesArray = cardPrefixes.Split(',');
            }

            if (cardPrefixesArray != null && cardPrefixesArray.Length > 0)
            {
                foreach (var member in members)
                {
                    foreach (var cardPrefix in cardPrefixesArray)
                    {
                        if (member.CardId.StartsWith(cardPrefix))
                        {
                            response.Add(member);
                            continue;
                        }
                    }
                }
            }

            return response;
        }

        private List<MemberInfoResponse> FilterPriorityFourFiveSix(List<MemberInfoResponse> members)
        {
            List<MemberInfoResponse> response = new List<MemberInfoResponse>();

            var cardPrefixes = _Configuration.GetSection("Card_Prefix_Rank").GetValue<string>("Rank_FourFiveSix");

            var cardPrefixesArray = Array.Empty<string>();

            if (!string.IsNullOrEmpty(cardPrefixes))
            {
                cardPrefixesArray = cardPrefixes.Split(',');
            }

            if (cardPrefixesArray != null && cardPrefixesArray.Length > 0)
            {
                foreach (var member in members)
                {
                    foreach (var cardPrefix in cardPrefixesArray)
                    {
                        if (member.CardId.StartsWith(cardPrefix))
                        {
                            response.Add(member);
                            continue;
                        }
                    }
                }
            }
            return response;
        }
        #endregion
    }
}

