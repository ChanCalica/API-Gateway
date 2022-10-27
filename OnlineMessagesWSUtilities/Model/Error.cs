using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OnlineMessagesWSUtilities.Model
{
    public class Error
    {
        [JsonProperty("@ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("@ErrorDescription")]
        public string ErrorDescription { get; set; }
    }

    public class ErrorMSG3
    {
        public Error Error { get; set; }
    }
}
