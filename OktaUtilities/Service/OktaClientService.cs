using API_Gateway.Common.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using OktaApiUtilities.Models;
using OktaApiUtilities.Models.Request;
using OktaUtilities.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OktaUtilities.Service
{
    public class OktaClientService : IOktaClientService
    {
        private readonly HttpClient _httpClient;

        static IConfiguration config = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());

        private static string OktaUri = config["OKTAURL:OktaURI"].ToString();

        private static string OktaAPIKey = config["OKTAURL:OktaAPIKey"].ToString();
        public OktaClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(OktaUri);

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            //_httpClient.DefaultRequestHeaders.Add(HeaderNames.ContentType, "application/json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "SSWS " + OktaAPIKey);
        }

        public async Task<User> GetUser(string userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<User>("/api/v1/users/" + userId);

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

        public async Task<List<User>?> SearchUser(string? mobileNumber, string? firstName, string? lastName, string? email)
        {
            try
            {
                string query = CreateQuery(mobileNumber, firstName, lastName, email);

                var response = await _httpClient.GetFromJsonAsync<List<User>>("/api/v1/users?search=" + query);

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

        public async Task<List<User>?> SearchUser(string? mobileNumber)
        {
            try
            {
                string query = CreateQuery(mobileNumber);

                var response = await _httpClient.GetFromJsonAsync<List<User>>("/api/v1/users?filter=" + query);

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

        public async Task<List<User>?> SearchUserUsingGRCardNumber(string? grCardNumber)
        {
            try
            {
                string query = CreateQueryWithGRCardNumber(grCardNumber);

                var response = await _httpClient.GetFromJsonAsync<List<User>>("/api/v1/users?search=" + query);

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

        public async Task<User> UpdateUser(string userId, UpdateUserRequest request)
        {
            try
            {
                User? user = new User();

                var response = await _httpClient.PostAsJsonAsync("/api/v1/users/" + userId, request);

                if (!response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    throw new ErrorException(HttpStatusCode.BadRequest, "Message: " + response.ReasonPhrase, "Okta Error");
                }

                if (response.Content != null)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(result))
                    {
                        throw new ErrorException(HttpStatusCode.BadRequest, "Message: No Content", "Okta Error");
                    }

                    user = JsonConvert.DeserializeObject<User>(result);
                }

                return user;
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

        private static string CreateQuery(string? mobileNumber, string? firstName, string? lastName, string? email)
        {
            string query = string.Empty;

            if (!string.IsNullOrEmpty(mobileNumber))
            {
                query = query + "profile.login eq \"" + mobileNumber + "\"";
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                if (!string.IsNullOrEmpty(query))
                {
                    query = query + " and ";
                }
                query = query + "profile.firstName eq \"" + firstName + "\"";
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                if (!string.IsNullOrEmpty(query))
                {
                    query = query + " and ";
                }
                query = query + "profile.lastName eq \"" + lastName + "\"";
            }

            if (!string.IsNullOrEmpty(email))
            {
                if (!string.IsNullOrEmpty(query))
                {
                    query = query + " and ";
                }
                query = query + "profile.email eq \"" + email + "\"";
            }

            if (!string.IsNullOrEmpty(query))
            {
                query = System.Web.HttpUtility.UrlEncode(query);
            }

            return query;
        }

        private static string CreateQuery(string? mobileNumber)
        {
            string query = string.Empty;

            if (!string.IsNullOrEmpty(mobileNumber))
            {
                query = query + "profile.login eq \"" + mobileNumber + "\"";
            }

            if (!string.IsNullOrEmpty(query))
            {
                query = System.Web.HttpUtility.UrlEncode(query);
            }

            return query;
        }

        private static string CreateQueryWithGRCardNumber(string? grCardNumPrim)
        {
            string query = string.Empty;

            if (!string.IsNullOrEmpty(grCardNumPrim))
            {
                query = query + "profile.grCardNumPrim eq \"" + grCardNumPrim + "\"";
            }

            if (!string.IsNullOrEmpty(query))
            {
                query = System.Web.HttpUtility.UrlEncode(query);
            }

            return query;
        }
    }
}
