using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway.Common.Data
{
    public class DemographicUpdateRequest
    {

        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("middleInitial")]
        public string? MiddleInitial { get; set; }

        [JsonProperty("birthDate")]
        public string? BirthDate { get; set; }

        [JsonProperty("mobilePhoneNumber")]
        public string? MobilePhoneNumber { get; set; }

        [JsonProperty("cardId")]
        public string? CardId { get; set; }


    }
}
