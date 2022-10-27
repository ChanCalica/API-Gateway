using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;

namespace API_Gateway.Common.Data
{
    public class ErrorResponse
    {
       
        public string ErrorCode { get; set; }
        public string ErrorId { get; set; }
        public List<string>  ErrorCauses { get; set; }
     
        public string? Type { get; set; }
     
        public string? CorrelationId { get; set; }
     
        public string? CurrentURL { get; set; }
   
        public string? Summary { get; set; }
    }
}
