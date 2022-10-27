using API_Gateway_FunctionApp.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway_FunctionApp.Service
{
    public class FunctionAppService : IFunctionAppService
    {
        private readonly HttpClient _httpClient;

        static IConfiguration config = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());

        private static string FunctionUri = config["FunctionAppURL:FunctionURI"].ToString();
        private static string FunctionAppkeys = config["FunctionAppURL:Appkeys"].ToString();

        public FunctionAppService(HttpClient httpClient)
        {

            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(FunctionUri);

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }

        public async Task<string?> GetCardId(string attributeId)
        {
            try
            { 
                var response = await _httpClient.GetAsync("/api/GetCardId?id=" + attributeId + "&code=" + FunctionAppkeys);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var cardId = string.IsNullOrEmpty(jsonResponse) ? null : jsonResponse.Trim('\"');

                return cardId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ResetCardId(string cardId)
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/OnFailResetCardStatus?cardId=" + cardId + "&code=" + FunctionAppkeys);

                var jsonResponse = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
