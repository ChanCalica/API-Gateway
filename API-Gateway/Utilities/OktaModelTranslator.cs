using API_Gateway.Models.Request;
using OktaApiUtilities.Models;
using OktaApiUtilities.Models.Request;

namespace API_Gateway.Utilities
{
    public class OktaModelTranslator
    {
        public static UpdateUserRequest CreateUserUpdateRequest(DemographicRequest request, string cardNumber, User oldUser)
        {
            UpdateUserRequest userRequest = new UpdateUserRequest();

            long cardId = long.Parse(cardNumber);

            userRequest.Profile = new Profile();

            userRequest.Profile.Login = oldUser.Profile.Login;
            userRequest.Profile.Email = oldUser.Profile.Email;
            userRequest.Profile.GRCardNumPrim = cardId == 0 ? oldUser.Profile.GRCardNumPrim : cardId;
            userRequest.Profile.PrimaryPhone = oldUser.Profile.PrimaryPhone;
            userRequest.Profile.MobilePhone = oldUser.Profile.MobilePhone;

            if (request.Member != null)
            {
                userRequest.Profile.DOB = string.IsNullOrEmpty(request.Member.BirthDate) ? oldUser.Profile.DOB : request.Member.BirthDate;
                userRequest.Profile.FirstName = string.IsNullOrEmpty(request.Member.FirstName) ? oldUser.Profile.FirstName : request.Member.FirstName;
                userRequest.Profile.LastName = string.IsNullOrEmpty(request.Member.LastName) ? oldUser.Profile.LastName : request.Member.LastName;

                if (!string.IsNullOrEmpty(request.Member.MiddleName))
                {
                    userRequest.Profile.MiddleName = request.Member.MiddleName[0].ToString().ToUpper();
                }
                else
                {
                    userRequest.Profile.MiddleName = string.IsNullOrEmpty(request.Member.MiddleInitial) ? oldUser.Profile.MiddleName : request.Member.MiddleInitial;
                }
      
            }

            return userRequest;
        }
    }
}
