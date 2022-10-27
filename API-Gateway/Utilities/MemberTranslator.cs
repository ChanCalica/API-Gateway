using API_Gateway.Common.Data;
using API_Gateway.Models.Request;
using API_Gateway.Models.Response;
using HQModels = LoyaltyOnlineWSUtilities.Models;
using Newtonsoft.Json;
using OnlineMessagesWSUtilities.Model;
using System.Net;
using System.Xml;
using LOWSUtilities = LoyaltyOnlineWSUtilities.Models;

namespace API_Gateway.Utilities
{
    public class MemberTranslator
    {
        public static HQModels.MemberModel SingleMemberDetails(string member)
        {
            var result = new HQModels.MemberModel();

            var singleMemberObject = XMLtoSingleMemberObject(member);

            result = singleMemberObject;

            return result;

        }
        public static HQModels.TransactionModel MemberTransactions(string transactions, string cardId, string limit)
        {
            var result = new HQModels.TransactionModel();

            var xmlResult = transactions.Split("><");

            if (xmlResult.Length == 1)
            {
                throw new ErrorException(HttpStatusCode.NotFound, "Not Found", "HQ Error");
            }

            if (xmlResult.Length > 3)
            {
                var transactionObject = XMLtoTransactionsObject(transactions);

                if (transactionObject.Transactions == null)
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                if (limit == "1")
                {
                    var oneTransaction = transactionObject.Transactions.Transaction.OrderByDescending(transaction => transaction.StartDateTime).Take(1).ToList();

                    result.Transactions = new HQModels.MemberTransactions();
                    result.Transactions.Transaction = new List<HQModels.MemberTransaction>();

                    if (oneTransaction != null && oneTransaction.Count > 0)
                    {
                        result.Transactions.Transaction = oneTransaction;
                    }

                }
                else
                {
                    result = transactionObject;

                }

            }
            else
            {
                var transactionObject = XMLtoTransactionObject(transactions);

                var singleTransaction = TranslateSingleTransaction(transactionObject);


                result.Transactions = new HQModels.MemberTransactions();
                result.Transactions.Transaction = new List<HQModels.MemberTransaction>();

                result.Transactions.Transaction.Add(singleTransaction);

            }

            var response = result;

            return response;

        }

        private static HQModels.MemberTransaction TranslateSingleTransaction(HQModels.SingleTransactionModel singleTransaction)
        {

            HQModels.MemberTransaction transaction = new HQModels.MemberTransaction();

            transaction.StoreId = singleTransaction.Transactions.Transaction.StoreId;
            transaction.PosDateTime = singleTransaction.Transactions.Transaction.PosDateTime;
            transaction.PosId = singleTransaction.Transactions.Transaction.PosId;
            transaction.TransId = singleTransaction.Transactions.Transaction.TransId;
            transaction.StartDateTime = singleTransaction.Transactions.Transaction.StartDateTime;
            transaction.ClubCardId = singleTransaction.Transactions.Transaction.ClubCardId;
            transaction.CreatedAt = singleTransaction.Transactions.Transaction.CreatedAt;
            transaction.UpdatedBy = singleTransaction.Transactions.Transaction.UpdatedBy;
            transaction.TotalAmount = singleTransaction.Transactions.Transaction.TotalAmount;
            transaction.StoreName = singleTransaction.Transactions.Transaction.StoreName;
            transaction.IsTransactionVoid = singleTransaction.Transactions.Transaction.IsTransactionVoid;

            return transaction;

        }
        public static HQModels.AccountTransactionModel AccountTransactions(string accountTransactions, string requestDate, int limit)
        {
            var result = new HQModels.AccountTransactionModel();

            var xmlResult = accountTransactions.Split("><");

            if (xmlResult.Length > 5)
            {
                var accountTransactionsObject = XMLtoAccountTransactionObject(accountTransactions);

                if (accountTransactionsObject.Transactions == null)
                {
                    throw new ErrorException(HttpStatusCode.NotFound, "Not Found");
                }

                int take = limit;

                if (take != 0)
                {
                    var limitTransaction = accountTransactionsObject.Transactions.Transaction.OrderByDescending(transaction => transaction.StartDateTime).Take(take).ToList();

                    result.Transactions = new HQModels.AccountTransactions();
                    result.Transactions.Transaction = new List<HQModels.AccountTransaction>();

                    if (limitTransaction != null && limitTransaction.Count > 0)
                    {
                        result.Transactions.Transaction = limitTransaction;
                    }

                }
                else
                {
                    var limitTransaction = accountTransactionsObject.Transactions.Transaction.OrderByDescending(transaction => transaction.StartDateTime).Take(10).ToList();

                    result.Transactions = new HQModels.AccountTransactions();
                    result.Transactions.Transaction = new List<HQModels.AccountTransaction>();

                    if (limitTransaction != null && limitTransaction.Count > 0)
                    {
                        result.Transactions.Transaction = limitTransaction;
                    }

                }

            }
            else
            {

                var accountTransactionsObject = XMLtoAccountSingleTransactionObject(accountTransactions);

                var singleTransaction = TranslateSingleAccountTransaction(accountTransactionsObject);

                result.Transactions = new HQModels.AccountTransactions();
                result.Transactions.Transaction = new List<HQModels.AccountTransaction>();

                result.Transactions.Transaction.Add(singleTransaction);

            }

            return result;
        }

        private static HQModels.AccountTransaction TranslateSingleAccountTransaction(HQModels.AccountSingleTransactionsModel transaction)
        {
            HQModels.AccountTransaction singleTransaction = new HQModels.AccountTransaction();

            singleTransaction.RetailerId = transaction.Transactions.Transaction.RetailerId;
            singleTransaction.StoreId = transaction.Transactions.Transaction.StoreId;
            singleTransaction.StoreName = transaction.Transactions.Transaction.StoreName;
            singleTransaction.PosDateTime = transaction.Transactions.Transaction.PosDateTime;
            singleTransaction.PosId = transaction.Transactions.Transaction.PosId;
            singleTransaction.TranId = transaction.Transactions.Transaction.TranId;
            singleTransaction.StartDateTime = transaction.Transactions.Transaction.StartDateTime;
            singleTransaction.ClubCardId = transaction.Transactions.Transaction.ClubCardId;
            singleTransaction.TransactionTotalAmount = transaction.Transactions.Transaction.TransactionTotalAmount;
            singleTransaction.IsTransactionVoid = transaction.Transactions.Transaction.IsTransactionVoid;
            singleTransaction.ReasonDescription = transaction.Transactions.Transaction.ReasonDescription;
            singleTransaction.ReasonCode = transaction.Transactions.Transaction.ReasonCode;
            singleTransaction.ReasonName = transaction.Transactions.Transaction.ReasonName;
            singleTransaction.ExternalTranId = transaction.Transactions.Transaction.ExternalTranId;

            singleTransaction.MemberAccount = TranslateMemberAccount(transaction);

            return singleTransaction;
        }

        private static HQModels.MemberAccount TranslateMemberAccount(HQModels.AccountSingleTransactionsModel accountTransaction)
        {
            HQModels.MemberAccount memberAccount = new HQModels.MemberAccount();

            memberAccount.Id = accountTransaction.Transactions.Transaction.MemberAccount.Id;
            memberAccount.EarnValue = accountTransaction.Transactions.Transaction.MemberAccount.EarnValue;
            memberAccount.RedeemValue = accountTransaction.Transactions.Transaction.MemberAccount.RedeemValue;
            memberAccount.InitialValue = accountTransaction.Transactions.Transaction.MemberAccount.InitialValue;
            memberAccount.RewardUsedAmount = accountTransaction.Transactions.Transaction.MemberAccount.RewardUsedAmount;
            memberAccount.RewardStatus = accountTransaction.Transactions.Transaction.MemberAccount.RewardStatus;

            return memberAccount;
        }

        public static GetPointsResponse MemberPoints(string cardNumber, string account)
        {
            var response = new GetPointsResponse();
            var accountObject = XMLtoAccountObject(account);

            if (accountObject.Accounts != null)
            {
                response.Balance = accountObject.Accounts.Account.Balance;
                //response.LastUpdated = DateTime.Parse(accountObject.Accounts.Account.LastUpdate);
                response.LastUpdated = accountObject.Accounts.Account.LastUpdate;
            }
            else
            {
                response.Balance = "";
                response.LastUpdated = "";
                //throw new ErrorException(HttpStatusCode.NotFound, "Not Found", "HQ Error");



            }
            response.CardId = cardNumber;
            return response;
        }

        public static MemberInfoResponse TranslateMember(string cardNumber, string household, string? account, string? tier)
        {
            var response = new MemberInfoResponse();

            var householdObject = XMLtoHouseholdObject(household);

            if (!string.IsNullOrEmpty(account))
            {

                var accountObject = XMLtoAccountObject(account);

                if (accountObject.Accounts != null)
                {
                    response.Balance = accountObject.Accounts.Account.Balance;
                    response.LastUpdated = accountObject.Accounts.Account.LastUpdate;
                }
                else
                {
                    response.Balance = null;
                    response.LastUpdated = null;
                }

            }

            response.CardId = cardNumber;
            response.AccountCreated = householdObject.HouseHold.Members.Member.Cards.Card.IssueDate;
            response.Status = householdObject.HouseHold.Members.Member.MemberStatus;

            if (string.IsNullOrEmpty(tier))
            {
                response.Tier = null;

            }
            else
            {
                response.Tier = TierLookUp(householdObject, tier);

            }


            response.Member = TranslateProfile(householdObject);

            response._Links = new Links();
            response._Links.MemberInfo = new MemberInfo();
            response._Links.MemberInfo.Method = "";
            response._Links.MemberInfo.Href = "";

            return response;
        }

        public static MSG3RequestObjects CreateMSG3RequestObjects(AdjustPointsRequest request)
        {
            MSG3RequestObjects msg3Request = new MSG3RequestObjects();

            if (request.Transactions != null && request.Transactions.Count > 0)
            {
                List<string> storeIds = new List<string>();

                foreach (var transaction in request.Transactions)
                {
                    if (!storeIds.Contains(transaction.StoreId))
                    {
                        storeIds.Add(transaction.StoreId);
                    }
                }

                foreach (var storeId in storeIds)
                {
                    var msg3ObjectRequest = CreateMSG3Object(request, storeId);

                    msg3Request.RequestObjects.Add(msg3ObjectRequest);
                }
            }

            return msg3Request;
        }

        public static MSG3RequestObjects CreateMSG3BatchRequestObjects(BatchPointsRequest request, string retailId)
        {
            MSG3RequestObjects msg3Request = new MSG3RequestObjects();

            if (request.Transactions != null && request.Transactions.Count > 0)
            {
                List<string> storeIds = new List<string>();

                var storeId = string.Empty;

                foreach (var transaction in request.Transactions)
                {
                   storeId = transaction.StoreId;

                    if (!storeIds.Contains(transaction.StoreId))
                    {
                        storeIds.Add(transaction.StoreId);
                    }
                }

                var msg3ObjectRequest = CreateMSG3BatchObject(request, retailId, storeId);

                msg3Request.RequestObjects.Add(msg3ObjectRequest);

            }

            return msg3Request;
        }



        public static MSG3RequestObject CreateMSG3Object(AdjustPointsRequest request, string storeId)
        {
            MSG3RequestObject requestObject = new MSG3RequestObject();

            requestObject.MessageData = CreateMessageDataXMLString(request, storeId);
            requestObject.Chain = request.RetailerId;
            requestObject.Branch = storeId;

            return requestObject;
        }

        public static MSG3RequestObject CreateMSG3BatchObject(BatchPointsRequest request, string retailId, string storeId)
        {
            MSG3RequestObject requestObject = new MSG3RequestObject();

            requestObject.MessageData = CreateMessageDataBatchXMLString(request, storeId);
            requestObject.Chain = retailId;
            requestObject.Branch = storeId;

            return requestObject;
        }

        public static string CreateMessageDataXMLString(AdjustPointsRequest request, string storeId)
        {
            var messageData = CreateMessageDataModel(request, storeId);

            string xmlText = MessageDataToXMLString(messageData);

            return xmlText;
        }

        public static string CreateMessageDataBatchXMLString(BatchPointsRequest request, string storeId)
        {
            var messageData = CreateMessageDataBatchModel(request, storeId);

            string xmlText = MessageDataToXMLString(messageData);

            return xmlText;
        }

        public static MessageData CreateMessageDataModel(AdjustPointsRequest request, string storeId)
        {
            MessageData messageData = new MessageData();

            messageData.LoyaltyUploadInterface = CreateLoyaltyUploadInterface(request, storeId);

            return messageData;
        }

        public static MessageData CreateMessageDataBatchModel(BatchPointsRequest request, string storeId)
        {
            MessageData messageData = new MessageData();

            messageData.LoyaltyUploadInterface = CreateLoyaltyUploadInterfaceForBatch(request, storeId);

            return messageData;
        }

        private static LoyaltyUploadInterface CreateLoyaltyUploadInterface(AdjustPointsRequest request, string storeId)
        {
            LoyaltyUploadInterface loyaltyUploadInterface = new LoyaltyUploadInterface();

            loyaltyUploadInterface.Root = CreateListRoots(request, storeId);

            return loyaltyUploadInterface;
        }
        private static LoyaltyUploadInterface CreateLoyaltyUploadInterfaceForBatch(BatchPointsRequest request, string storeId)
        {
            LoyaltyUploadInterface loyaltyUploadInterface = new LoyaltyUploadInterface();

            loyaltyUploadInterface.Root = CreateListRootsForBatch(request, storeId);

            return loyaltyUploadInterface;
        }

        private static List<Root> CreateListRoots(AdjustPointsRequest request, string storeId)
        {
            List<Root> roots = new List<Root>();

            if (request.Transactions != null && request.Transactions.Count > 0)
            {
                foreach (var transaction in request.Transactions)
                {
                    if (storeId.Equals(transaction.StoreId))
                    {
                        Root root = CreateRootModel(transaction, request.CardId, request.RetailerId, request.PosId, request.CashierId);

                        roots.Add(root);
                    }
                }
            }

            return roots;
        }

        private static List<Root> CreateListRootsForBatch(BatchPointsRequest request, string storeId)
        {
            List<Root> roots = new List<Root>();

            if (request.Transactions != null && request.Transactions.Count > 0)
            {
                foreach (var transaction in request.Transactions)
                {

                    Root root = CreateRootModelForBatch(transaction, transaction.CardId, request.RetailerId, transaction.PosId, transaction.CashierId);

                    roots.Add(root);
                    
                }
            }

            return roots;
        }

        private static Root CreateRootModel(Transaction transaction, string cardId, string retailerId, string posId, string cashierId)
        {
            Root root = new Root();

            root.Customer = CreateCustomer(transaction, cardId, retailerId, posId, cashierId);

            return root;
        }

        private static Root CreateRootModelForBatch(BatchTransaction transaction, string cardId, string retailerId, string posId, string cashierId)
        {
            Root root = new Root();

            root.Customer = CreateCustomerForBatch(transaction, cardId, retailerId, posId, cashierId);

            return root;
        }

        private static FilteredTransactions CreateFilterDateModel(TransactionsRequest request)
        {
            FilteredTransactions filter = new FilteredTransactions();

            filter.Filter = CreateFilterDate(request);

            return filter;
        }

        private static TransactionsRequest CreateFilterDate(TransactionsRequest request)
        {
            TransactionsRequest transactions = new TransactionsRequest();
            if (!string.IsNullOrEmpty(request.MaxRows))
            {
                if (request.MaxRows == "1")
                {
                    transactions.MaxRows = "2";
                }
                else
                {
                    transactions.MaxRows = request.MaxRows;
                }

            }
            else
            {
                transactions.MaxRows = "10";
            }

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                transactions.StartDate = request.StartDate;
            }

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                transactions.EndDate = request.EndDate;
            }

            return transactions;
        }

        private static AccountDetailedRequest CreateAccountDetailed(AccountDetailedRequest request)
        {
            AccountDetailedRequest accountDetailed = new AccountDetailedRequest();

            accountDetailed.FilteredAccountDetailedTransactions = CreateFilteredAccountDetailed(request);

            return accountDetailed;

        }

        private static FilteredAccountDetailedTransactions CreateFilteredAccountDetailed(AccountDetailedRequest request)
        {
            FilteredAccountDetailedTransactions FilteredAccountDetailed = new FilteredAccountDetailedTransactions();

            FilteredAccountDetailed.SearchCriteria = CreateSearchCriteria(request);
            FilteredAccountDetailed.Member = CreateMember(request);

            return FilteredAccountDetailed;
        }

        private static member CreateMember(AccountDetailedRequest request)
        {
            member member = new member();

            member.ClubCardId = request.FilteredAccountDetailedTransactions.Member.ClubCardId;
            member.Account = CreateAccount(request);

            return member;
        }

        private static Account CreateAccount(AccountDetailedRequest request)
        {
            Account account = new Account();

            account.Id = request.FilteredAccountDetailedTransactions.Member.Account.Id;

            return account;
        }

        private static SearchCriteria CreateSearchCriteria(AccountDetailedRequest request)
        {
            SearchCriteria criteria = new SearchCriteria();

            criteria.TransactionDates = CreateTransactionDates(request);
            //criteria.AccountActivityType = CreateAccountActivityType(request);


            return criteria;

        }

        private static TransactionDates CreateTransactionDates(AccountDetailedRequest request)
        {
            TransactionDates dates = new TransactionDates();
            if (!string.IsNullOrEmpty(request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.StartDate))
            {
                if (!string.IsNullOrEmpty(request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.EndDate))
                {
                    dates.StartDate = request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.StartDate;
                    dates.EndDate = request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.EndDate;


                }


            }



            return dates;

        }

        //private static AccountActivityType CreateAccountActivityType(AccountDetailedRequest request)
        //{
        //    AccountActivityType type = new AccountActivityType();
        //    if (!string.IsNullOrEmpty(request.FilteredAccountDetailedTransactions.SearchCriteria.AccountActivityType.Type))
        //    {
        //        type.Type = request.FilteredAccountDetailedTransactions.SearchCriteria.AccountActivityType.Type;

        //    }


        //    return type;

        //}

        private static Customer CreateCustomer(Transaction transaction, string cardId, string retailerId, string posId, string cashierId)
        {
            Customer customer = new Customer();

            customer.MsgType = "3";
            customer.LPEVer = "R10";
            customer.RetailerId = retailerId;
            customer.StoreID = transaction.StoreId;
            customer.StartDateTime = transaction.TransTime;
            customer.PosID = posId;
            customer.CashierID = cashierId;
            customer.HomeStore = "1";
            customer.TransID = transaction.TransId.ToString();
            customer.TicketIdentifier = "1";
            customer.BusinessDate = transaction.TransTime.Date.ToString();
            customer.TicketTotal = string.IsNullOrEmpty(transaction.TicketTotal) ? "0.0" : transaction.TicketTotal;

            customer.LoyaltyInfo = CreateLoyaltyInfo(transaction, cardId);
            return customer;
        }

        private static Customer CreateCustomerForBatch(BatchTransaction transaction, string cardId, string retailerId, string posId, string cashierId)
        {
            Customer customer = new Customer();

            customer.MsgType = "3";
            customer.LPEVer = "R10";
            customer.RetailerId = retailerId;
            customer.StoreID = transaction.StoreId;
            customer.StartDateTime = transaction.TransTime;
            customer.PosID = posId;
            customer.CashierID = cashierId;
            customer.HomeStore = "1";
            customer.TransID = transaction.TransId.ToString();
            customer.TicketIdentifier = "1";
            customer.BusinessDate = transaction.TransTime.Date.ToString();
            customer.TicketTotal = string.IsNullOrEmpty(transaction.TicketTotal) ? "0.0" : transaction.TicketTotal;

            customer.LoyaltyInfo = CreateLoyaltyInfoForBatch(transaction, cardId);
            return customer;
        }
        private static LoyaltyInfo CreateLoyaltyInfo(Transaction transaction, string cardId)
        {
            LoyaltyInfo loyaltyInfo = new LoyaltyInfo();

            loyaltyInfo.ServerDate = transaction.TransTime.Date;
            loyaltyInfo.CardID = cardId;
            loyaltyInfo.Accounts = CreateAccounts(transaction);

            return loyaltyInfo;
        }

        private static LoyaltyInfo CreateLoyaltyInfoForBatch(BatchTransaction transaction, string cardId)
        {
            LoyaltyInfo loyaltyInfo = new LoyaltyInfo();

            loyaltyInfo.ServerDate = transaction.TransTime.Date;
            loyaltyInfo.CardID = cardId;
            loyaltyInfo.Accounts = CreateAccountsForBatch(transaction);

            return loyaltyInfo;
        }

        private static Accounts CreateAccounts(Transaction transaction)
        {
            Accounts accounts = new Accounts();
            accounts.Acc = new Acc()
            {
                ID = "2",
                EarnValue = transaction.EarnValue,
                RdmValue = transaction.RedeemValue,
                ReasonCode = "90"
            };

            return accounts;
        }

        private static Accounts CreateAccountsForBatch(BatchTransaction transaction)
        {
            Accounts accounts = new Accounts();
            accounts.Acc = new Acc()
            {
                ID = "2",
                EarnValue = transaction.EarnValue,
                ReasonCode = "90"
            };

            return accounts;
        }

        public static string CreateHouseholdXMLString(string householdModelString, DemographicRequest request)
        {
            LOWSUtilities.HouseholdModel householdModel = CreateHouseholdModel(householdModelString, request);

            string xmlText = HouseholdModelToXMLString(householdModel);

            return xmlText;
        }

        public static LOWSUtilities.HouseholdModel CreateHouseholdModel(string householdModelString, DemographicRequest request)
        {
            var householdModel = XMLtoHouseholdObject(householdModelString);

            var newHouseholdModel = new LOWSUtilities.HouseholdModel();

            newHouseholdModel.HouseHold = CreateHouseholdObject(request, householdModel.HouseHold);
            newHouseholdModel.Xml = CreateHouseholdXMLObject();

            return newHouseholdModel;
        }

        public static string CreateHouseholdXMLString(DemographicRequest request)
        {
            LOWSUtilities.HouseholdModel householdModel = CreateHouseholdModel(request);

            string xmlText = HouseholdModelToXMLString(householdModel);

            return xmlText;
        }

        public static LOWSUtilities.HouseholdModel CreateHouseholdModel(DemographicRequest request)
        {
            LOWSUtilities.HouseholdModel householdModel = new LOWSUtilities.HouseholdModel();

            householdModel.HouseHold = CreateHouseholdObject(request);
            householdModel.Xml = CreateHouseholdXMLObject();

            return householdModel;
        }

        private static LOWSUtilities.Household CreateHouseholdObject(DemographicRequest request)
        {
            LOWSUtilities.Household household = new LOWSUtilities.Household();

            household.BuyingUnitInternalKey = 0;
            household.HouseHoldExternalId = request.Member.CardId;
            household.EMailAddress = request.EmailAddress;
            household.Street1 = request.Street1;
            household.Street2 = request.Street2;
            household.City = ConcatCityProvince(request.City, request.Province);
            household.PostalCode = request.PostalCode;
            household.Country = string.IsNullOrEmpty(request.Country) ? "46" : request.Country;
            household.POBox = request.POBox;
            household.HomePhone = request.HomePhone;
            household.Members = CreateHouseholdMembers(request);

            return household;
        }

        private static LOWSUtilities.Household CreateHouseholdObject(DemographicRequest request, LOWSUtilities.Household household)
        {
            LOWSUtilities.Household newHousehold = new LOWSUtilities.Household();

            newHousehold.BuyingUnitInternalKey = household.BuyingUnitInternalKey;
            newHousehold.HouseHoldExternalId = string.IsNullOrEmpty(household.HouseHoldExternalId) ? "0" : household.HouseHoldExternalId;
            newHousehold.Country = string.IsNullOrEmpty(request.Country) ? household.Country : request.Country;

            if (string.IsNullOrEmpty(request.City) && string.IsNullOrEmpty(request.Province))
            {
                newHousehold.City = household.City;
            }
            else
            {
                newHousehold.City = ConcatCityProvince(request.City, request.Province, household.City);
            }
           
            newHousehold.Street1 = string.IsNullOrEmpty(request.Street1) ? household.Street1 : request.Street1;
            newHousehold.Street2 = string.IsNullOrEmpty(request.Street2) ? household.Street2 : request.Street2;
            newHousehold.StreetNum = string.IsNullOrEmpty(request.StreetNum) ? household.StreetNum : request.StreetNum;
            newHousehold.PostalCode = string.IsNullOrEmpty(request.PostalCode) ? household.PostalCode : request.PostalCode;
            newHousehold.POBox = string.IsNullOrEmpty(request.POBox) ? household.POBox : request.POBox;
            newHousehold.PhonePrefix = string.IsNullOrEmpty(request.PhonePrefix) ? household.PhonePrefix : request.PhonePrefix;
            newHousehold.HomePhone = string.IsNullOrEmpty(request.HomePhone) ? household.HomePhone : request.HomePhone;
            newHousehold.EMailAddress = string.IsNullOrEmpty(request.EmailAddress) ? household.EMailAddress : request.EmailAddress;
            newHousehold.SendEmail = string.IsNullOrEmpty(request.SendEmail) ? household.SendEmail : request.SendEmail;
            newHousehold.County = string.IsNullOrEmpty(request.County) ? household.County : request.County;
            newHousehold.HouseName = string.IsNullOrEmpty(request.HouseName) ? household.HouseName : request.HouseName;
            newHousehold.Members = CreateHouseholdMembers(request, household.Members);

            return newHousehold;
        }

        private static LOWSUtilities.Members CreateHouseholdMembers(DemographicRequest request)
        {
            LOWSUtilities.Members members = new LOWSUtilities.Members();

            members.Member = new LOWSUtilities.Member();

            members.Member.MemberInternalKey = "0";
            members.Member.MemberExternalId = request.Member.CardId;
            members.Member.FirstName = request.Member.FirstName;
            members.Member.LastName = request.Member.LastName;
            members.Member.AdditionalFirstName = request.Member.AdditionalFirstName;
            members.Member.AdditionalLastName = request.Member.AdditionalLastName;
            members.Member.BirthDate = request.Member.BirthDate;
            members.Member.DriversLicense = request.Member.DriversLicense;
            members.Member.NationalInsuranceNumber = request.Member.NationalInsuranceNumber;
            members.Member.Remarks = request.Member.Remarks;
            members.Member.MobilePhoneNumber = request.Member.MobilePhoneNumber;
            members.Member.WorkPhoneNumber = request.Member.WorkPhoneNumber;
            members.Member.Gender = request.Member.Gender;
            members.Member.Passport = request.Member.Passport;
            members.Member.StartDate = request.Member.StartDate == DateTime.MinValue ? DateTime.Now : request.Member.StartDate;
            members.Member.EffectiveDate = request.Member.EffectiveDate ?? DateTime.Now;
            members.Member.RedemptionPrivileges = request.Member.RedemptionPrivileges;
            members.Member.LanguageId = request.Member.LanguageId;
            members.Member.NumberOfFamilyMembers = request.Member.NumberOfFamilyMembers;
            members.Member.EMailAddress = request.Member.EmailAddress;
            members.Member.IsMainMember = "true";
            members.Member.MemberStatus = "1";
            members.Member.Cards = CreateHouseholdCards(request.Member.CardId);
            members.Member.MemberAttributes = CreateMemberAttributes(request.Member.MemberAttribute);

            if (!string.IsNullOrEmpty(request.Member.MiddleName))
            {
                members.Member.MiddleInitial = request.Member.MiddleName[0].ToString().ToUpper();
                members.Member.MemberAttributes = AddMemberAttribute(request.Member.MiddleName, "10130", "String", members.Member.MemberAttributes);
                members.Member.MemberAttributes = AddMemberAttribute(members.Member.MiddleInitial, "10020", "String", members.Member.MemberAttributes);
            }
            else if (!string.IsNullOrEmpty(request.Member.MiddleInitial))
            {
                members.Member.MiddleInitial = request.Member.MiddleInitial;
                members.Member.MemberAttributes = AddMemberAttribute(request.Member.MiddleInitial, "10130", "String", members.Member.MemberAttributes);
                members.Member.MemberAttributes = AddMemberAttribute(request.Member.MiddleInitial, "10020", "String", members.Member.MemberAttributes);
            }

            if (!string.IsNullOrEmpty(request.Member.AltMobileNumber))
            {
                members.Member.MemberAttributes = AddMemberAttribute(request.Member.AltMobileNumber, "10028", "String", members.Member.MemberAttributes);
            }
            else if (!string.IsNullOrEmpty(request.Member.MobilePhoneNumber))
            {
                members.Member.MemberAttributes = AddMemberAttribute(request.Member.MobilePhoneNumber, "10028", "String", members.Member.MemberAttributes);
            }

            if (!string.IsNullOrEmpty(request.Member.CivilStatus))
            {
                members.Member.MemberAttributes = AddMemberAttribute(request.Member.CivilStatus, "10005", "Integer", members.Member.MemberAttributes);
            }

            return members;
        }

        private static LOWSUtilities.Members CreateHouseholdMembers(DemographicRequest request, LOWSUtilities.Members members)
        {
            LOWSUtilities.Members newMembers = new LOWSUtilities.Members();

            newMembers.Member = new LOWSUtilities.Member();

            if (request.Member == null)
            {
                request.Member = new DemographicMemberRequest();
            }

            newMembers.Member.MemberInternalKey = string.IsNullOrEmpty(members.Member.MemberInternalKey) ? "0" : members.Member.MemberInternalKey;
            newMembers.Member.MemberExternalId = members.Member.MemberExternalId;
            newMembers.Member.FirstName = string.IsNullOrEmpty(request.Member.FirstName) ? members.Member.FirstName : request.Member.FirstName;
            newMembers.Member.LastName = string.IsNullOrEmpty(request.Member.LastName) ? members.Member.LastName : request.Member.LastName;
            newMembers.Member.AdditionalFirstName = string.IsNullOrEmpty(request.Member.AdditionalFirstName) ? members.Member.AdditionalFirstName : request.Member.AdditionalFirstName;
            newMembers.Member.AdditionalLastName = string.IsNullOrEmpty(request.Member.AdditionalLastName) ? members.Member.AdditionalLastName : request.Member.AdditionalLastName;
            newMembers.Member.MiddleInitial = string.IsNullOrEmpty(request.Member.MiddleInitial) ? members.Member.MiddleInitial : request.Member.MiddleInitial;
            newMembers.Member.DriversLicense = string.IsNullOrEmpty(request.Member.DriversLicense) ? members.Member.DriversLicense : request.Member.DriversLicense;
            newMembers.Member.NationalInsuranceNumber = string.IsNullOrEmpty(request.Member.NationalInsuranceNumber) ? members.Member.NationalInsuranceNumber : request.Member.NationalInsuranceNumber;
            newMembers.Member.Remarks = string.IsNullOrEmpty(request.Member.Remarks) ? members.Member.Remarks : request.Member.Remarks;
            newMembers.Member.MobilePhoneNumber = members.Member.MobilePhoneNumber;
            newMembers.Member.WorkPhoneNumber = string.IsNullOrEmpty(request.Member.WorkPhoneNumber) ? members.Member.WorkPhoneNumber : request.Member.WorkPhoneNumber;
            newMembers.Member.Gender = string.IsNullOrEmpty(request.Member.Gender) ? members.Member.Gender : request.Member.Gender;
            newMembers.Member.Passport = string.IsNullOrEmpty(request.Member.Passport) ? members.Member.Passport : request.Member.Passport;
            newMembers.Member.StartDate = request.Member.StartDate == DateTime.MinValue ? members.Member.StartDate : request.Member.StartDate;
            newMembers.Member.EffectiveDate = request.Member.EffectiveDate ?? members.Member.EffectiveDate;
            newMembers.Member.RedemptionPrivileges = string.IsNullOrEmpty(request.Member.RedemptionPrivileges) ? members.Member.RedemptionPrivileges : request.Member.RedemptionPrivileges;
            newMembers.Member.LanguageId = string.IsNullOrEmpty(request.Member.LanguageId) ? members.Member.LanguageId : request.Member.LanguageId;
            newMembers.Member.NumberOfFamilyMembers = string.IsNullOrEmpty(request.Member.NumberOfFamilyMembers) ? members.Member.NumberOfFamilyMembers : request.Member.NumberOfFamilyMembers;
            newMembers.Member.EMailAddress = members.Member.EMailAddress;
            newMembers.Member.IsMainMember = string.IsNullOrEmpty(request.Member.IsMainMember) ? members.Member.IsMainMember : request.Member.IsMainMember;
            newMembers.Member.MemberStatus = string.IsNullOrEmpty(request.Member.MemberStatus) ? members.Member.MemberStatus : request.Member.MemberStatus;
            
            newMembers.Member.Cards = CreateHouseholdCards(members.Member.Cards);
            newMembers.Member.MemberAttributes = CreateMemberAttributes(request.Member.MemberAttribute, members.Member.MemberAttributes);

            DateTime.TryParse(members.Member.BirthDate, out DateTime bdate);
            if (string.IsNullOrEmpty(members.Member.BirthDate) || bdate == DateTime.MinValue)
            {
                newMembers.Member.BirthDate = request.Member.BirthDate;
            }
            else
            {
                newMembers.Member.BirthDate = members.Member.BirthDate;
            }

            if (!string.IsNullOrEmpty(request.Member.MiddleName))
            {
                newMembers.Member.MiddleInitial = request.Member.MiddleName[0].ToString().ToUpper();
                newMembers.Member.MemberAttributes = AddMemberAttribute(request.Member.MiddleName, "10130", "String", members.Member.MemberAttributes);
                newMembers.Member.MemberAttributes = AddMemberAttribute(newMembers.Member.MiddleInitial, "10020", "String", members.Member.MemberAttributes);
            }
            else if (!string.IsNullOrEmpty(request.Member.MiddleInitial))
            {
                newMembers.Member.MiddleInitial = request.Member.MiddleInitial;
                newMembers.Member.MemberAttributes = AddMemberAttribute(request.Member.MiddleInitial, "10130", "String", members.Member.MemberAttributes);
                newMembers.Member.MemberAttributes = AddMemberAttribute(request.Member.MiddleInitial, "10020", "String", members.Member.MemberAttributes);
            }
            else
            {
                newMembers.Member.MiddleInitial = members.Member.MiddleInitial;
            }

            if (!string.IsNullOrEmpty(request.Member.AltMobileNumber))
            {
                newMembers.Member.MemberAttributes = AddMemberAttribute(request.Member.AltMobileNumber, "10028", "String", members.Member.MemberAttributes);
            }
            else if (!string.IsNullOrEmpty(members.Member.MobilePhoneNumber))
            {
                newMembers.Member.MemberAttributes = AddMemberAttribute(members.Member.MobilePhoneNumber, "10028", "String", members.Member.MemberAttributes);
            }

            if (!string.IsNullOrEmpty(request.Member.CivilStatus))
            {
                newMembers.Member.MemberAttributes = AddMemberAttribute(request.Member.CivilStatus, "10005", "Integer", members.Member.MemberAttributes);
            }

            return newMembers;
        }

        private static LOWSUtilities.Cards CreateHouseholdCards(string cardNumber)
        {
            LOWSUtilities.Cards cards = new LOWSUtilities.Cards();

            cards.Card = new LOWSUtilities.Card();

            cards.Card.Id = cardNumber;
            cards.Card.CardStatus = "1";
            cards.Card.IssueDate = DateTime.Now;
            cards.Card.ExpirationDate = DateTime.Parse("2056-12-31");
            cards.Card.EffectiveDate = DateTime.Now;

            return cards;
        }

        private static LOWSUtilities.Cards CreateHouseholdCards(LOWSUtilities.Cards memberCards)
        {
            LOWSUtilities.Cards cards = new LOWSUtilities.Cards();

            cards.Card = new LOWSUtilities.Card();

            cards.Card.Id = memberCards.Card.Id;
            cards.Card.CardStatus = memberCards.Card.CardStatus;
            cards.Card.IssueDate = memberCards.Card.IssueDate;
            cards.Card.ExpirationDate = memberCards.Card.ExpirationDate;
            cards.Card.BarcodeId = memberCards.Card.BarcodeId;
            cards.Card.EffectiveDate = memberCards.Card.EffectiveDate;

            return cards;
        }

        private static LOWSUtilities.MemberAttributes AddMemberAttribute(string attributeValue, string attributeCode, string attributeDataType, LOWSUtilities.MemberAttributes memberAttributes)
        {
            if (memberAttributes != null)
            {
                if (memberAttributes.Attribute != null && memberAttributes.Attribute.Count > 0)
                {
                    if (memberAttributes.Attribute.Exists(o => o.Id.Equals(attributeCode)))
                    {
                        foreach (var attribute in memberAttributes.Attribute)
                        {
                            if (attribute.Id.Equals(attributeCode))
                            {
                                attribute.Value = attributeValue;
                            }
                        }
                    }
                    else
                    {
                        LOWSUtilities.Attribute newAttribute = new LOWSUtilities.Attribute();

                        newAttribute.Id = attributeCode;
                        newAttribute.DataType = attributeDataType;
                        newAttribute.Value = attributeValue;

                        memberAttributes.Attribute.Add(newAttribute);
                    }
                }
            }

            return memberAttributes;
        }

        private static LOWSUtilities.MemberAttributes CreateMemberAttributes(PartnerCode request)
        {
            LOWSUtilities.MemberAttributes attributes = new LOWSUtilities.MemberAttributes();
            attributes.Attribute = new List<LOWSUtilities.Attribute>();

            if (request != null)
            {
                LOWSUtilities.Attribute attribute = new LOWSUtilities.Attribute();

                attribute.Id = request.Id;
                attribute.DataType = "Integer";
                attribute.Value = string.IsNullOrEmpty(request.Value) ? "1" : request.Value;

                attributes.Attribute.Add(attribute);

                if (!string.IsNullOrEmpty(request.Id))
                {
                    attributes.Attribute.Add(CreateAttributeHistory(request));

                    if (request.Id.Equals("10153"))
                    {
                        attributes.Attribute.Add(PartnerBusinessRule(request));
                    }
                }


            }

            return attributes;
        }

        private static LOWSUtilities.Attribute PartnerBusinessRule(PartnerCode request)
        {
            LOWSUtilities.Attribute attribute = new LOWSUtilities.Attribute();

            attribute.Id = "10021";
            attribute.DataType = "Integer";
            attribute.Value = "4";


            return attribute;
        }


        private static LOWSUtilities.Attribute CreateAttributeHistory(PartnerCode request)
        {
            LOWSUtilities.Attribute attribute = new LOWSUtilities.Attribute();

            var tierValue = "1";

            attribute.Id = "10121";
            attribute.DataType = "String";
            attribute.Value = DateTime.Now.ToString("yyyyMMdd") + "=" + tierValue;

            return attribute;

        }

        private static LOWSUtilities.MemberAttributes CreateMemberAttributes(PartnerCode request, LOWSUtilities.MemberAttributes memberAttributes)
        {
            bool attributeAlreadyExists = false;


            if (request != null)
            {
                if (memberAttributes != null)
                {
                    if (memberAttributes.Attribute != null)
                    {
                        var attributeHistory = memberAttributes.Attribute.FirstOrDefault(o => o.Id.Equals("10121"));

                        if (attributeHistory != null)
                        {
                            var memberAttribute = memberAttributes.Attribute.FirstOrDefault(o => o.Id.Equals("10021"));

                            if (memberAttribute != null)
                            {
                                var prevTier = memberAttribute.Value;

                                string[] attributeArray = new string[] { attributeHistory.Value };

                                string attributeValue = DateTime.Now.ToString("yyyyMMdd") + "=" + prevTier;

                                attributeArray = new List<string>(attributeArray) { attributeValue }.ToArray();

                                string result = string.Join(",", attributeArray);

                                string[] memberAttributeList = result.Split(",");

                                List<string> attributeList = new List<string>(memberAttributeList);


                                if (memberAttributeList.Length > 5)
                                {
                                    attributeList.RemoveAt(0);

                                }

                                string resultValue = string.Join(",", attributeList);
                                attributeHistory.Value = resultValue;


                            }
                            else
                            {
                                memberAttributes.Attribute.Add(PartnerBusinessRule(request));
                            }


                        }
                        else
                        {
                            memberAttributes.Attribute.Add(CreateAttributeHistory(request));

                        }

                        if (memberAttributes.Attribute.Count > 0)
                        {
                            foreach (var attribute in memberAttributes.Attribute.ToList())
                            {
                                if (attribute.Id == request.Id)
                                {
                                    attribute.Value = request.Value;
                                    attributeAlreadyExists = true;
                                }
                            }

                        }

                        if (request.Id.Equals("10153"))
                        {
                            if (request.Value.Equals("1") || request.Value.Equals("2"))
                            {
                                var attribute = memberAttributes.Attribute.FirstOrDefault(o => o.Id.Equals("10021"));

                                if (attribute != null)
                                {
                                    if (attribute.Value != "2" && attribute.Value != "4" && attribute.Value != "7")
                                    {
                                        var value = "4";
                                        attribute.Value = value;

                                    }

                                }
                                else
                                {
                                    memberAttributes.Attribute.Add(PartnerBusinessRule(request));
                                }
                            }

                            if (request.Value.Equals("0"))
                            {
                                var attribute = memberAttributes.Attribute.FirstOrDefault(o => o.Id.Equals("10021"));

                                if (attribute != null)
                                {
                                    if (attribute.Value != "2" && attribute.Value != "7")
                                    {

                                        var value = "1";
                                        attribute.Value = value;

                                    }
                                }

                            }

                        }


                        if (!attributeAlreadyExists)
                        {
                            LOWSUtilities.Attribute attribute = new LOWSUtilities.Attribute();

                            attribute.Id = request.Id;
                            attribute.DataType = "Integer";
                            attribute.Value = string.IsNullOrEmpty(request.Value) ? "1" : request.Value;

                            memberAttributes.Attribute.Add(attribute);
                        }

                    }

                }

            }
            return memberAttributes;
        }

        private static LOWSUtilities.Xml CreateHouseholdXMLObject()
        {
            LOWSUtilities.Xml xmlObject = new LOWSUtilities.Xml();

            xmlObject.Version = "1.0";
            xmlObject.Encoding = "UTF-8";

            return xmlObject;
        }

        private static Profile TranslateProfile(LOWSUtilities.HouseholdModel household)
        {
            var profile = new Profile();

            profile.FirstName = string.IsNullOrWhiteSpace(household.HouseHold.Members.Member.FirstName) ? household.HouseHold.Members.Member.FirstName : household.HouseHold.Members.Member.FirstName.Trim();
            profile.LastName = string.IsNullOrWhiteSpace(household.HouseHold.Members.Member.LastName) ? household.HouseHold.Members.Member.LastName : household.HouseHold.Members.Member.LastName.Trim();
            profile.MiddleInitial = string.IsNullOrWhiteSpace(household.HouseHold.Members.Member.MiddleInitial) ? household.HouseHold.Members.Member.MiddleInitial : household.HouseHold.Members.Member.MiddleInitial.Trim();
            profile.Gender = household.HouseHold.Members.Member.Gender;

            if (household.HouseHold.Members.Member.BirthDate != null)
            {
                profile.BirthDate = DateTime.Parse(household.HouseHold.Members.Member.BirthDate);
            }

            profile.MobileNumber = household.HouseHold.Members.Member.MobilePhoneNumber;
            profile.EmailAddress = string.IsNullOrEmpty(household.HouseHold.Members.Member.EMailAddress) ? household.HouseHold.EMailAddress : household.HouseHold.Members.Member.EMailAddress;
            profile.Country = household.HouseHold.Country;
            profile.City = GetCity(household.HouseHold.City);
            profile.Province = GetProvince(household.HouseHold.City);
            profile.Street1 = household.HouseHold.Street1;
            profile.Street2 = household.HouseHold.Street2;
            profile.StreetNum = household.HouseHold.StreetNum;
            profile.PostalCode = household.HouseHold.PostalCode;
            profile.POBox = household.HouseHold.POBox;
            profile.HomePhone = household.HouseHold.HomePhone;

            if (household.HouseHold.Members.Member.MemberAttributes != null)
            {
                var middleName = GetAttributeValue(household.HouseHold.Members.Member.MemberAttributes, "10130");

                if (!string.IsNullOrEmpty(middleName))
                {
                    profile.MiddleName = middleName.Trim();
                }

                var civilStatus = GetAttributeValue(household.HouseHold.Members.Member.MemberAttributes, "10005");

                if (!string.IsNullOrEmpty(civilStatus))
                {
                    profile.CivilStatus = civilStatus;
                }

                var altMobileNumber = GetAttributeValue(household.HouseHold.Members.Member.MemberAttributes, "10028");

                if (!string.IsNullOrEmpty(altMobileNumber))
                {
                    profile.AltMobileNumber = altMobileNumber;
                }
            }

            return profile;
        }
        private static LOWSUtilities.MemberModel XMLtoSingleMemberObject(string json)
        {
            var jsonTransaction = XMLtoJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.MemberModel>(jsonTransaction);

            return result;
        }

        private static string GetAttributeValue(LOWSUtilities.MemberAttributes memberAttributes, string attributeCode)
        {
            string attributeValue = string.Empty;

            if (memberAttributes.Attribute != null && memberAttributes.Attribute.Count > 0)
            {
                foreach (var attribute in memberAttributes.Attribute)
                {
                    if (attribute.Id.Equals(attributeCode))
                    {
                        attributeValue = attribute.Value;
                    }
                }
            }

            return attributeValue;
        }

        private static LOWSUtilities.TransactionModel XMLtoTransactionsObject(string json)
        {
            var jsonTransaction = XMLtoJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.TransactionModel>(jsonTransaction);

            return result;
        }

        private static LOWSUtilities.SingleTransactionModel XMLtoTransactionObject(string json)
        {
            var jsonTransaction = XMLtoJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.SingleTransactionModel>(jsonTransaction);

            return result;
        }

        private static LOWSUtilities.AccountTransactionModel XMLtoAccountTransactionObject(string json)
        {
            var jsonTransaction = XMLtoJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.AccountTransactionModel>(jsonTransaction);

            return result;
        }

        private static LOWSUtilities.AccountSingleTransactionsModel XMLtoAccountSingleTransactionObject(string json)
        {
            var jsonTransaction = XMLtoJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.AccountSingleTransactionsModel>(jsonTransaction);

            return result;
        }
        private static LOWSUtilities.AccountModel XMLtoAccountObject(string json)
        {
            var jsonAccount = XMLtoJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.AccountModel>(jsonAccount);

            return result;
        }

        public static LOWSUtilities.HouseholdModel XMLtoHouseholdObject(string json)
        {
            var jsonHousehold = XMLtoHouseholdJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.HouseholdModel>(jsonHousehold);

            return result;
        }

        private static string HouseholdModelToXMLString(LOWSUtilities.HouseholdModel householdModel)
        {
            string jsonText = JsonConvert.SerializeObject(householdModel,
                                                          new JsonSerializerSettings
                                                          {
                                                              NullValueHandling = NullValueHandling.Ignore
                                                          });

            string xmlText = JSONtoXML(jsonText);

            return xmlText;
        }

        private static string MessageDataToXMLString(MessageData messageData)
        {
            string jsonText = JsonConvert.SerializeObject(messageData,
                                                          new JsonSerializerSettings
                                                          {
                                                              NullValueHandling = NullValueHandling.Ignore
                                                          });

            string xmlText = JSONtoXML(jsonText);

            return xmlText;
        }

        public static string FilteredTransactionsToXMLString(TransactionsRequest FilteredDate)
        {
            var date = CreateFilterDateModel(FilteredDate);

            string jsonText = JsonConvert.SerializeObject(date,
                                                          new JsonSerializerSettings
                                                          {
                                                              NullValueHandling = NullValueHandling.Ignore
                                                          });

            string xmlText = FilteredDateJSONtoXML(jsonText);

            return xmlText;
        }

        public static string AccountTransactionsToXMLString(AccountDetailedRequest accountRequest)
        {
            var accountJSON = CreateAccountDetailed(accountRequest);

            string jsonText = JsonConvert.SerializeObject(accountJSON,
                                                          new JsonSerializerSettings
                                                          {
                                                              NullValueHandling = NullValueHandling.Ignore
                                                          });

            string xmlText = JSONtoXML(jsonText);

            return xmlText;
        }

        private static string GetTierValue(LOWSUtilities.HouseholdModel household)
        {
            var result = string.Empty;
            var attributes = household.HouseHold.Members.Member.MemberAttributes.Attribute;

            foreach (var attribute in attributes)
            {
                int valueHolder = 0;
                if (attribute.Id.Equals("10138"))
                {
                    result = attribute.Value;
                    break;
                }
                else if (attribute.Id.Equals("10021"))
                {
                    int newValue = 0;
                    int.TryParse(attribute.Value, out newValue);
                    if (newValue > valueHolder)
                    {
                        valueHolder = newValue;
                        result = valueHolder.ToString();
                    }
                }
                else if (attribute.Id.Equals("10153") && string.IsNullOrEmpty(result))
                {
                    result = attribute.Value;

                }
            }

            return result;
        }
        private static string TierLookUp(LOWSUtilities.HouseholdModel household, string tier)
        {
            var result = string.Empty;
            var attributes = household.HouseHold.Members.Member.MemberAttributes.Attribute;


            foreach (var attribute in attributes)
            {
                if (attribute.Id.Equals(tier))
                {
                    result = attribute.Value;
                    break;
                }

            }

            return result;
        }


        private static string XMLtoJSON(string xmlText)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(xmlText);

            string jsonText = JsonConvert.SerializeXmlNode(xml);

            return jsonText;
        }

        private static string XMLtoHouseholdJSON(string xmlText)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlText);

            var memberAttributeNode = xml.GetElementsByTagName("Attribute");

            if (memberAttributeNode.Count == 1)
            {
                var attribute = xml.CreateAttribute("json", "Array", "http://james.newtonking.com/projects/json");
                attribute.InnerText = "true";
                var node = memberAttributeNode.Item(0) as XmlElement;
                node.Attributes.Append(attribute);
            }

            string jsonText = JsonConvert.SerializeXmlNode(xml);

            return jsonText;
        }

        private static string JSONtoXML(string jsonText)
        {
            string xmlText = string.Empty;

            XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonText);
            xmlText = doc.InnerXml;

            return xmlText;
        }

        private static string FilteredDateJSONtoXML(string jsonText)
        {
            string xmlText = string.Empty;

            XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonText);
            xmlText = doc.InnerXml;

            return xmlText;
        }

        private static string ConcatCityProvince(string? city, string? province)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(city))
            {
                result = city;
            }

            if (!string.IsNullOrWhiteSpace(province))
            {
                result += ", " + province;
            }

            return result;
        }

        private static string ConcatCityProvince(string? city, string? province, string? cityProvince)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(cityProvince))
            {
                string[] places = cityProvince.Split(", ");

                //Updates if there is both City and Province
                if (places.Length > 1)
                {
                    //check if there is a city request, replaces data if there is one
                    if (!string.IsNullOrWhiteSpace(city))
                    {
                        result = city;
                    }
                    else
                    {
                        result = places[0];
                    }

                    //check if there is a province request, replaces data if there is one
                    if (!string.IsNullOrWhiteSpace(province))
                    {
                        result += ", " + province;
                    }
                    else
                    {
                        result += ", " + places[1];
                    }
                }
                //assumes that there is only a city and no province indicated
                else if (places.Length == 1)
                {
                    //check if there is a city request, replaces data if there is one
                    if (!string.IsNullOrWhiteSpace(city))
                    {
                        result = city;
                    }
                    else
                    {
                        result = places[0];
                    }

                    //check if there is a province request, appends the data
                    if (!string.IsNullOrWhiteSpace(province))
                    {
                        result += ", " + province;
                    }
                }
            }
            else
            {
                //checks if there is city data before trying to update
                if (!string.IsNullOrWhiteSpace(city))
                {
                    //if there is no initial data, creates one
                    result = ConcatCityProvince(city, province);
                }
                else
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "City is required when Province is provided.");
                }
            }

            return result;
        }

        private static string GetCity(string? cityProvince)
        {
            var city = string.Empty;

            if (!string.IsNullOrWhiteSpace(cityProvince))
            {
                string[] places = cityProvince.Split(", ");

                city = places[0];
            }

            return city;
        }

        private static string GetProvince(string? cityProvince)
        {
            var province = string.Empty;

            if (!string.IsNullOrWhiteSpace(cityProvince))
            {
                string[] places = cityProvince.Split(", ");

                //check if there is a province 
                if (places.Length > 1)
                {
                    province = places[1];
                }
            }

            return province;
        }

        public static string SanitizeMobileNumber(string mobileNumber)
        {
            string sanitizedNumber = mobileNumber.Substring(mobileNumber.Length - 10);

            return sanitizedNumber;
        }

        public static string SanitizeMobileNumberAddPrefix(string mobileNumber)
        {
            string sanitizedNumber = mobileNumber.Substring(mobileNumber.Length - 10);

            sanitizedNumber = "63" + sanitizedNumber;

            return sanitizedNumber;
        }
        public static CreateResponse CreatedMember(DemographicRequest request)
        {
            var response = new CreateResponse();

            response.Status = "Created";
            response.CreatedDate = DateTime.Now;
            response.Member = TranslateCreateMember(request);


            return response;
        }

        public static CreatedMember TranslateCreateMember(DemographicRequest request)
        {
            var response = new CreatedMember();
            //response.LastName = request.Member.LastName;
            //response.FirstName = request.Member.FirstName;
            //response.MiddleInitial = request.Member.MiddleInitial;
            //response.MiddleName = request.Member.MiddleName;
            //response.BirthDate = request.Member.BirthDate;
            //response.Remarks = request.Member.Remarks;
            //response.MobilePhoneNumber = request.Member.MobilePhoneNumber;
            //response.AltMobileNumber = request.Member.AltMobileNumber;
            //response.Gender = request.Member.Gender;
            //response.CompanyName = request.Member.CompanyName;
            //response.EmailAddress = request.Member.EmailAddress;
            response.CardId = request.Member.CardId;

            return response;
        }

        #region Duplicate for Member Demographic

        public static MemberInfoResponse TranslateDemographicMember(string cardNumber, string household, string? account, string? tier)
        {
            var response = new MemberInfoResponse();

            var householdObject = XMLtoHouseholdMemberObject(household);

            var member = GetActiveMemberDemographic(householdObject.HouseHold.Members);

            if (!string.IsNullOrEmpty(account))
            {
                var accountObject = XMLtoAccountObject(account);

                if (accountObject.Accounts != null)
                {
                    response.Balance = accountObject.Accounts.Account.Balance;
                    response.LastUpdated = accountObject.Accounts.Account.LastUpdate;
                }
                else
                {
                    response.Balance = null;
                    response.LastUpdated = null;
                }
            }

            response.CardId = cardNumber;
            response.AccountCreated = member.Cards.Card.IssueDate;
            response.Status = member.Cards.Card.CardStatus;

            if (string.IsNullOrEmpty(tier))
            {
                response.Tier = null;
            }
            else
            {
                response.Tier = TierLookUp(householdObject, tier);
            }

            response.Member = TranslateProfile(householdObject);

            response._Links = new Links();
            response._Links.MemberInfo = new MemberInfo();
            response._Links.MemberInfo.Method = "";
            response._Links.MemberInfo.Href = "";

            return response;
        }

        private static Profile TranslateProfile(LOWSUtilities.HouseholdModelMemberDemographic household)
        {
            var profile = new Profile();

            var member = GetActiveMemberDemographic(household.HouseHold.Members);

            profile.FirstName = string.IsNullOrWhiteSpace(member.FirstName) ? member.FirstName : member.FirstName.Trim();
            profile.LastName = string.IsNullOrWhiteSpace(member.LastName) ? member.LastName : member.LastName.Trim();
            profile.MiddleInitial = string.IsNullOrWhiteSpace(member.MiddleInitial) ? member.MiddleInitial : member.MiddleInitial.Trim();
            profile.Gender = member.Gender;
            profile.MobileNumber = member.MobilePhoneNumber;
            profile.EmailAddress = string.IsNullOrEmpty(member.EMailAddress) ? household.HouseHold.EMailAddress : member.EMailAddress;
            profile.Country = household.HouseHold.Country;
            profile.City = GetCity(household.HouseHold.City);
            profile.Province = GetProvince(household.HouseHold.City);
            profile.Street1 = household.HouseHold.Street1;
            profile.Street2 = household.HouseHold.Street2;
            profile.StreetNum = household.HouseHold.StreetNum;
            profile.PostalCode = household.HouseHold.PostalCode;
            profile.POBox = household.HouseHold.POBox;
            profile.HomePhone = household.HouseHold.HomePhone;

            DateTime.TryParse(member.BirthDate, out DateTime bdate);
            if (bdate != DateTime.MinValue)
            {
                profile.BirthDate = bdate;
            }

            if (member.MemberAttributes != null)
            {
                var middleName = GetAttributeValue(member.MemberAttributes, "10130");

                if (!string.IsNullOrEmpty(middleName))
                {
                    profile.MiddleName = middleName.Trim();
                }

                var civilStatus = GetAttributeValue(member.MemberAttributes, "10005");

                if (!string.IsNullOrEmpty(civilStatus))
                {
                    profile.CivilStatus = civilStatus;
                }

                var altMobileNumber = GetAttributeValue(member.MemberAttributes, "10028");

                if (!string.IsNullOrEmpty(altMobileNumber))
                {
                    profile.AltMobileNumber = altMobileNumber;
                }
            }

            return profile;
        }

        private static string GetAttributeValue(HQModels.MemberAttributesMemberDemographic memberAttributes, string attributeCode)
        {
            string attributeValue = string.Empty;

            if (memberAttributes.Attribute != null && memberAttributes.Attribute.Count > 0)
            {
                foreach (var attribute in memberAttributes.Attribute)
                {
                    if (attribute.Id.Equals(attributeCode))
                    {
                        attributeValue = attribute.Value;
                    }
                }
            }

            return attributeValue;
        }

        private static string TierLookUp(LOWSUtilities.HouseholdModelMemberDemographic household, string tier)
        {
            var result = string.Empty;

            var member = GetActiveMemberDemographic(household.HouseHold.Members);

            var attributes = member.MemberAttributes.Attribute;

            foreach (var attribute in attributes)
            {
                if (attribute.Id.Equals(tier))
                {
                    result = attribute.Value;
                    break;
                }

            }

            return result;
        }

        public static LOWSUtilities.MemberMemberDemographic GetActiveMemberDemographic(LOWSUtilities.MembersMemberDemographic members)
        {
            var activeMember = new LOWSUtilities.MemberMemberDemographic();

            if (members != null)
            {
                if (members.Member != null && members.Member.Count > 0)
                {
                    foreach (var currentMember in members.Member)
                    {
                        if (currentMember.Cards.Card.CardStatus.Equals("1"))
                        {
                            activeMember = currentMember;
                        }
                    }
                }
            }

            return activeMember;
        }

        public static LOWSUtilities.HouseholdModelMemberDemographic XMLtoHouseholdMemberObject(string json)
        {
            var jsonHousehold = XMLtoHouseholdJSON(json);

            var result = JsonConvert.DeserializeObject<LOWSUtilities.HouseholdModelMemberDemographic>(jsonHousehold);

            return result;
        }
        public static MemberInfoResponse TranslateDemographicMemberforOkta(string cardNumber, string household, string? account, string? tier)
        {
            var response = new MemberInfoResponse();

            var householdObject = XMLtoHouseholdMemberObject(household);

            var member = GetActiveMemberDemographicforOkta(householdObject.HouseHold.Members, cardNumber);

            if (!string.IsNullOrEmpty(account))
            {
                var accountObject = XMLtoAccountObject(account);

                if (accountObject.Accounts != null)
                {
                    response.Balance = accountObject.Accounts.Account.Balance;
                    response.LastUpdated = accountObject.Accounts.Account.LastUpdate;
                }
                else
                {
                    response.Balance = null;
                    response.LastUpdated = null;
                }
            }

            response.CardId = cardNumber;
            response.AccountCreated = member.Cards.Card.IssueDate;
            response.Status = member.Cards.Card.CardStatus;

            if (string.IsNullOrEmpty(tier))
            {
                response.Tier = null;
            }
            else
            {
                response.Tier = TierLookUpforOkta(householdObject, tier, cardNumber);
            }

            response.Member = TranslateProfileforOkta(householdObject, cardNumber);

            response._Links = new Links();
            response._Links.MemberInfo = new MemberInfo();
            response._Links.MemberInfo.Method = "";
            response._Links.MemberInfo.Href = "";

            return response;
        }
        private static Profile TranslateProfileforOkta(LOWSUtilities.HouseholdModelMemberDemographic household, string cardNumber)
        {
            var profile = new Profile();

            var member = GetActiveMemberDemographicforOkta(household.HouseHold.Members, cardNumber);

            profile.FirstName = string.IsNullOrWhiteSpace(member.FirstName) ? member.FirstName : member.FirstName.Trim();
            profile.LastName = string.IsNullOrWhiteSpace(member.LastName) ? member.LastName : member.LastName.Trim();
            profile.MiddleInitial = string.IsNullOrWhiteSpace(member.MiddleInitial) ? member.MiddleInitial : member.MiddleInitial.Trim();
            profile.Gender = member.Gender;
            profile.MobileNumber = member.MobilePhoneNumber;
            profile.EmailAddress = string.IsNullOrEmpty(member.EMailAddress) ? household.HouseHold.EMailAddress : member.EMailAddress;
            profile.Country = household.HouseHold.Country;
            profile.City = GetCity(household.HouseHold.City);
            profile.Province = GetProvince(household.HouseHold.City);
            profile.Street1 = household.HouseHold.Street1;
            profile.Street2 = household.HouseHold.Street2;
            profile.StreetNum = household.HouseHold.StreetNum;
            profile.PostalCode = household.HouseHold.PostalCode;
            profile.POBox = household.HouseHold.POBox;
            profile.HomePhone = household.HouseHold.HomePhone;

            DateTime.TryParse(member.BirthDate, out DateTime bdate);
            if (bdate != DateTime.MinValue)
            {
                profile.BirthDate = bdate;
            }

            if (member.MemberAttributes != null)
            {
                var middleName = GetAttributeValue(member.MemberAttributes, "10130");

                if (!string.IsNullOrEmpty(middleName))
                {
                    profile.MiddleName = middleName.Trim();
                }

                var civilStatus = GetAttributeValue(member.MemberAttributes, "10005");

                if (!string.IsNullOrEmpty(civilStatus))
                {
                    profile.CivilStatus = civilStatus;
                }

                var altMobileNumber = GetAttributeValue(member.MemberAttributes, "10028");

                if (!string.IsNullOrEmpty(altMobileNumber))
                {
                    profile.AltMobileNumber = altMobileNumber;
                }
            }

            return profile;
        }
        private static string TierLookUpforOkta(LOWSUtilities.HouseholdModelMemberDemographic household, string tier, string cardNumber)
        {
            var result = string.Empty;

            var member = GetActiveMemberDemographicforOkta(household.HouseHold.Members, cardNumber);

            var attributes = member.MemberAttributes.Attribute;

            foreach (var attribute in attributes)
            {
                if (attribute.Id.Equals(tier))
                {
                    result = attribute.Value;
                    break;
                }

            }

            return result;
        }
        public static LOWSUtilities.MemberMemberDemographic GetActiveMemberDemographicforOkta(LOWSUtilities.MembersMemberDemographic members, string cardNumber)
        {
            var activeMember = new LOWSUtilities.MemberMemberDemographic();

            if (members != null)
            {
                if (members.Member != null && members.Member.Count > 0)
                {
                    foreach (var currentMember in members.Member)
                    {
                        if(currentMember.MemberExternalId == cardNumber)
                        {
                            if (currentMember.Cards.Card.CardStatus.Equals("1"))
                            {
                                activeMember = currentMember;
                            }
                        }
                       
                    }
                }
            }

            return activeMember;
        }
        #endregion
    }
}
