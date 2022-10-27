using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway.Common.Data
{
    public class ErrorException : Exception
    {
        public string? ErrorCode { get; set; }
        public string? ErrorId { get; set; }
        public string? Type { get; set; }
        public string? Referrer { get; set; }
        public string? CurrentURL { get; set; }
        public string? Summary { get; set; }
        public List<string>? ErrorCauses { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ErrorException() : base()
        {
        }

        public ErrorException(HttpStatusCode statusCode) : base()
        {
            StatusCode = statusCode;
        }

        public ErrorException(HttpStatusCode statusCode, string cause) : base()
        {
            StatusCode = statusCode;
            ErrorCauses = AddErrorCause(cause);
        }

        public ErrorException(HttpStatusCode statusCode, string cause, string type) : base()
        {
            StatusCode = statusCode;
            ErrorCauses = AddErrorCause(cause);
            Type = type;
        }

        public ErrorException(HttpStatusCode statusCode, List<string> causes, string type) : base()
        {
            StatusCode = statusCode;
            ErrorCauses = AddErrorCause(causes);
            Type = type;
        }

        public ErrorException(string errorCause) : base(errorCause)
        {
            ErrorCode = "E000026";
            ErrorId = Guid.NewGuid().ToString();
            ErrorCauses = AddErrorCause(errorCause);
        }

        
        private List<string> AddErrorCause(string error)
        {
            List<string> causes = new List<string>();

            if (!string.IsNullOrEmpty(error))
            {
                causes.Add(error);
            }

            return causes;
            //return new { resultSet = error};
        }

        private List<string> AddErrorCause(List<string> errors)
        {
            List<string> causes = new List<string>();

            if (errors != null && errors.Count > 0)
            {
                causes.AddRange(errors);
            }

            return causes;
        }
    }
}
