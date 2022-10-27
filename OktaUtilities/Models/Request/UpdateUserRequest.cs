using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaApiUtilities.Models.Request
{
    public class UpdateUserRequest
    {
        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }
}
