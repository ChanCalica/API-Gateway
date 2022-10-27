using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Gateway.Common.Data
{
    public class ErrorsResponse
    {
        [JsonIgnore]
        public string ErrorCode { get; set; }
        public string ErrorId { get; set; }
        public List<string> ErrorCauses { get; set; }
        [JsonIgnore]
        public string? Type { get; set; }
        [JsonIgnore]
        public string? CorrelationId { get; set; }
        [JsonIgnore]
        public string? CurrentURL { get; set; }
        [JsonIgnore]
        public string? Summary { get; set; }

    }
}
