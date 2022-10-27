using API_Gateway.Common.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LoyaltyOnlineWSLogin;
using LoyaltyOnlineWSUtilities.Service.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyOnlineWSUtilities.Service
{
    public class LOWSLoginService : ILOWSLoginService
    {
        private readonly SecretClient _secretClient;
        public LOWSLoginService(SecretClient secretClient)

        {
            _secretClient = secretClient;

        }

        static IConfiguration config = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());

        private static string URL = config["HQWSURL:URL"].ToString();

        private static string CompleteURL = URL + "Authorization/Login.asmx";

        private LoginSoap? _loyaltyOnlineWSLoginClient;

        BasicHttpsBinding binding = new BasicHttpsBinding();
        EndpointAddress address =  new EndpointAddress(CompleteURL);

        private LoginSoap LoyaltyOnlineWSLoginClient
        {
            get
            {
                if (_loyaltyOnlineWSLoginClient == null)
                {
                    _loyaltyOnlineWSLoginClient = new LoginSoapClient(binding, address);
                }

                return _loyaltyOnlineWSLoginClient;
            }
        }


        public async Task<UserLoginResponse> Login(string username, string password)
        {
            try
            {
               

                var request = new UserLoginRequest(username, password);
                
                var response = await LoyaltyOnlineWSLoginClient.UserLoginAsync(request);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Login()
        {
            try
            {
                string usernameValue = string.Empty;
                string passwordValue = string.Empty;
                
                var username = "HQWSUsername";
                var password = "HQWSPassword";

                ObjectCache cache = MemoryCache.Default;

                var isExists = cache.Contains("Username");

                if (!isExists)
                {
                    usernameValue = await GetSecret(username);
                    passwordValue = await GetSecret(password);

                    var cacheUsername = new CacheItem("Username", usernameValue);
                    var cachePassword = new CacheItem("Password", passwordValue);

                    var cacheItemPolicy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(120)
                    };

                    cache.Add(cacheUsername, cacheItemPolicy);
                    cache.Add(cachePassword, cacheItemPolicy);

                    var valueUsername = cache.Get("Username");
                    var valuePassword = cache.Get("Password");

                    usernameValue = valueUsername.ToString();
                    passwordValue = valuePassword.ToString();
                }
                else
                {
                    var valueUsername = cache.Get("Username");
                    var valuePassword = cache.Get("Password");

                    usernameValue = valueUsername.ToString();
                    passwordValue = valuePassword.ToString();
                }

                var request = new UserLoginRequest(usernameValue, passwordValue);

                var response = await LoyaltyOnlineWSLoginClient.UserLoginAsync(request);

                //if (response.UserLoginResult.Status1 != ReMAEStatusType.Ok)
                //{
                //    throw new ErrorException(HttpStatusCode.BadRequest, response.UserLoginResult.Message, "HQ Error");
                //}

                if (string.IsNullOrEmpty(response.out_SessionKey))
                {
                    throw new ErrorException(HttpStatusCode.BadRequest, response.UserLoginResult.Message, "HQ Error");
                }

                return response.out_SessionKey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Logout()
        {
           await LoyaltyOnlineWSLoginClient.LogOutAsync();
        }

        private async Task<string> GetSecret(string username)

        {
            try

            {
                KeyVaultSecret keyValueSecret = await

                _secretClient.GetSecretAsync(username);

                return keyValueSecret.Value;
            }

            catch

            {
                throw;
            }
        }
    }
}
