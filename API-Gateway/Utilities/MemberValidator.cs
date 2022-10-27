using API_Gateway.Common.Data;
using API_Gateway.Models.Request;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace API_Gateway.Utilities
{
    public class MemberValidator
    {
        public static void CreateMemberRequestValidator(DemographicRequest request)
        {
            if (!string.IsNullOrEmpty(request.Street1))
            {
                int stringLength = request.Street1.Length;

                if (stringLength > 40)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Street1 should be less than 40 characters");
                }
            }

            if (!string.IsNullOrEmpty(request.Street2))
            {
                int stringLength = request.Street2.Length;

                if (stringLength > 400)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Street2 should be less than 400 characters");
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Province))
            {
                if (string.IsNullOrWhiteSpace(request.City))
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "City is Required.");
                }
            }

            if (string.IsNullOrEmpty(request.Member.LastName))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "LastName is Required");
            }

            if (string.IsNullOrEmpty(request.Member.MobilePhoneNumber))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "MobilePhoneNumber is Required");
            }

            if (string.IsNullOrEmpty(request.Member.BirthDate))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "BirthDate is Required");
            }

            if (!DateTime.TryParse(request.Member.BirthDate, out DateTime birthDate))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "BirthDate is not a Date");
            }

            if (string.IsNullOrEmpty(request.Member.CardId))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "CardId is Required");
            }

            if (!string.IsNullOrEmpty(request.Member.MiddleInitial))
            {
                if (request.Member.MiddleInitial.Length > 1)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Middle Initial must be a single character.");
                }
            }
        }

        public static void ValidateMobileNumber(string mobileNumber)
        {
            if (mobileNumber.Length < 10)
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "MobileNumber should be more than 9 digits");
            }

            if (!Regex.IsMatch(mobileNumber, @"^\d+$"))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "MobileNumber has non-numeric characters");
            }

        }

        public static void ValidateMobilePhoneNumber(DemographicRequest request)
        {
            if (request.Member != null)
            {
                if (!string.IsNullOrEmpty(request.Member.MobilePhoneNumber))
                {
                    if (request.Member.MobilePhoneNumber.Length < 10)
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "MobileNumber should be more than 9 digits");

                    }


                    if (!Regex.IsMatch(request.Member.MobilePhoneNumber, @"^\d+$"))
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "MobileNumber has non-numeric characters");
                    }

                    string mobileNumber = request.Member.MobilePhoneNumber;

                    string mobile = mobileNumber.Substring(0, 1);
                    string Mobile = mobileNumber.Substring(0, 2);
                    if (mobile.Equals("0") || Mobile.Equals("00"))
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "Mobile number format is invalid");

                    }

                    if (!string.IsNullOrEmpty(request.Member.WorkPhoneNumber))
                    {
                        if (!Regex.IsMatch(request.Member.WorkPhoneNumber, @"^\d+$"))
                        {
                            throw new ErrorException(HttpStatusCode.BadRequest, "MobileNumber has non-numeric characters");
                        }
                    }
                }
            }
        }

        public static void UpdateMemberRequestValidator(DemographicRequest request)
        {
            if (!string.IsNullOrEmpty(request.Street1))
            {
                int stringLength = request.Street1.Length;

                if (stringLength > 40)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Street1 should be less than 40 characters");
                }
            }

            if (!string.IsNullOrEmpty(request.Street2))
            {
                int stringLength = request.Street2.Length;

                if (stringLength > 400)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Street2 should be less than 400 characters");
                }
            }

            if (request.Member != null && request.Member.MemberAttribute != null)
            {
              
                    if (string.IsNullOrEmpty(request.Member.MemberAttribute.Id))
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "Member Attribute Id is Required");
                    }

                    if (string.IsNullOrEmpty(request.Member.MemberAttribute.Value))
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "Member Attribute Value is Required");
                    }
          
            }

            if (request.Member != null && request.Member.MemberAttribute != null)
            {
                if (!string.IsNullOrEmpty(request.Member.MiddleInitial))
                {
                    if (request.Member.MiddleInitial.Length > 1)
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "Middle Initial must be a single character.");
                    }
                }
            }
        }

        public static void DisallowPIIChange(DemographicRequest request)
        {
            if (!string.IsNullOrEmpty(request.Member.EmailAddress))
            {
                throw new ErrorException(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
            }

            if (!string.IsNullOrEmpty(request.Member.FirstName))
            {
                throw new ErrorException(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
            }

            if (!string.IsNullOrEmpty(request.Member.LastName))
            {
                throw new ErrorException(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
            }

            if (!string.IsNullOrEmpty(request.Member.MiddleInitial))
            {
                throw new ErrorException(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
            }

            if (!string.IsNullOrEmpty(request.Member.BirthDate))
            {
                throw new ErrorException(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
            }

            if (!string.IsNullOrEmpty(request.Member.MobilePhoneNumber))
            {
                throw new ErrorException(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
            }
        }

        public static void ValidateAdjustPointsRequest(AdjustPointsRequest request)
        {
            if (request.Transactions == null || request.Transactions.Count == 0)
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "Transactions is Required");
            }

            foreach (Transaction transaction in request.Transactions)
            {
                if (transaction.TransId.ToString().Length > 9)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Max length should be 9 digits.");
                }

                if (string.IsNullOrEmpty(transaction.EarnValue) && string.IsNullOrEmpty(transaction.RedeemValue))
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "EarnValue/RedeemValue is Required");
                }

                if (!string.IsNullOrEmpty(transaction.EarnValue) && !string.IsNullOrEmpty(transaction.RedeemValue))
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Must only have one EarnValue/RedeemValue");
                }

                if (!string.IsNullOrEmpty(transaction.TicketTotal))
                {
                    if (!double.TryParse(transaction.TicketTotal, out double ticketTotal))
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "Ticket Total should be numeric");
                    }
                }
            }
        }

        public static void ValidateBatchPointsRequest(BatchPointsRequest request)
        {
           
            if (request.Transactions == null || request.Transactions.Count == 0)
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "Transactions is Required");
            }

            var hashSet = new HashSet<int>();

            foreach (BatchTransaction transId in request.Transactions)
            {
              
                if (!hashSet.Add(transId.TransId))
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "List contains duplicate values of TransId.");

                }
            }

            foreach (BatchTransaction transaction in request.Transactions)
            {

                if (transaction.EarnValue == "0")
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "EarnValue should not be Zero");
                }

                if (transaction.TransId.ToString().Length > 9)
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "Max length should be 9 digits.");
                }

                if (string.IsNullOrEmpty(transaction.EarnValue))
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, "EarnValue is Required");
                }
       
                if (!string.IsNullOrEmpty(transaction.TicketTotal))
                {
                    if (!double.TryParse(transaction.TicketTotal, out double ticketTotal))
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "Ticket Total should be numeric");
                    }
                }
            }
        }

        public static void ValidateFilterRequest(CheckMemberRequest request)
        {
            if (request.BirthDate != null)
            {
                if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.LastName))
                {
                    throw new ErrorException(HttpStatusCode.Forbidden, "Email Or Lastname is Required");
                }
            }
        }

        public static void CardValidate(string cardId)
        {
            if (cardId.Length != 16)
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "Card ID should be 16 digits");
            }
        }

        public static void ValidateBirthdate(string bday)
        {
            if (!DateTime.TryParse(bday, out DateTime bdate))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, "Birthday must be a date.");
            }
        }
    }
}
