using API_Gateway.Common.Data;
using LoyaltyOnlineWSMember;
using LoyaltyOnlineWSUtilities.Service.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LoyaltyOnlineWSUtilities.Service
{
    public class LOWSMemberService : ILOWSMemberService
    {
        private MemberWebServiceSoap _loyaltyOnlineWSMemberService;

        static IConfiguration config = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());

        private static string URL = config["HQWSURL:URL"].ToString();

        private static string CompleteURL = URL + "CRM/MemberService.asmx";

        BasicHttpsBinding binding = new BasicHttpsBinding();
        EndpointAddress address = new EndpointAddress(CompleteURL);

        private MemberWebServiceSoap LoyaltyOnlineWSMemberClient
        {
            get
            {
                if (_loyaltyOnlineWSMemberService == null)
                {
                    _loyaltyOnlineWSMemberService = new MemberWebServiceSoapClient(binding, address);
                }

                return _loyaltyOnlineWSMemberService;
            }
        }

        public async Task<GetDemographicResponse> GetDemographics(string cardNumber, string sessionKey)
        {
            try
            {
                string newAddress = CompleteURL + "?sk=" + sessionKey;
                address = new EndpointAddress(newAddress);
                var request = new GetDemographicRequest(cardNumber);

                var response = await LoyaltyOnlineWSMemberClient.GetDemographicAsync(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetHouseHoldAccountsActivityResponse> GetHouseholdAccountActivity(string cardNumber, string sessionKey)
        {
            try
            {
                string newAddress = CompleteURL + "?sk=" + sessionKey;

                address = new EndpointAddress(newAddress);

                var request = new GetHouseHoldAccountsActivityRequest(cardNumber);

                var response = await LoyaltyOnlineWSMemberClient.GetHouseHoldAccountsActivityAsync(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Status> SaveDemographics(string cardNumber, string household, string sessionKey)
        {
            try
            {
                string newAddress = CompleteURL + "?sk=" + sessionKey;
                address = new EndpointAddress(newAddress);

                var response = await LoyaltyOnlineWSMemberClient.SaveDemographicAsync(cardNumber, household);

                if (response.Status1 != ReMAEStatusType.Ok)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Message: " + response.Message, "HQ Error");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetHouseHoldFilteredTransactionsResponse> GetHouseHoldFilteredTransactions(string cardId,  string sessionKey, string transactionXMLString, string requestDate, string limit)
        {
            try
            {
                requestDate = transactionXMLString;

                string xmlFilter = transactionXMLString;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(requestDate);
                XmlNode newNode = doc.DocumentElement;
                
                string newAddress = CompleteURL + "?sk=" + sessionKey;

                address = new EndpointAddress(newAddress);

                var request = new GetHouseHoldFilteredTransactionsRequest(cardId, newNode);

                var response = await LoyaltyOnlineWSMemberClient.GetHouseHoldFilteredTransactionsAsync(request);

                return response;

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<GetFilteredMemberAccountDetailedTransactionsResponse> GetFilteredMemberAccountDetailedTransactions(string account, string sessionKey)
        {
            try
            {
                string newAddress = CompleteURL + "?sk=" + sessionKey;

                address = new EndpointAddress(newAddress);

                var request = new GetFilteredMemberAccountDetailedTransactionsRequest(account);

                var response = await LoyaltyOnlineWSMemberClient.GetFilteredMemberAccountDetailedTransactionsAsync(request);

                return response;

            }
            catch(Exception ex)
            {

                throw ex;
            }


        }

        public async Task<GetHouseHoldMembersDemographicResponse> GetHouseholdMembers(string cardId, string sessionKey)
        {
            try
            {
                string newAddress = CompleteURL + "?sk=" + sessionKey;
                address = new EndpointAddress(newAddress);

                var request = new GetHouseHoldMembersDemographicRequest(cardId);

                var response = await LoyaltyOnlineWSMemberClient.GetHouseHoldMembersDemographicAsync(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CardValidateResponse> CardValidate(string cardId, string sessionKey)
        {
            try
            {
                string newAddress = CompleteURL + "?sk=" + sessionKey;
                address = new EndpointAddress(newAddress);

                var request = new CardValidateRequest(cardId);

                var response = await LoyaltyOnlineWSMemberClient.CardValidateAsync(request);

                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
