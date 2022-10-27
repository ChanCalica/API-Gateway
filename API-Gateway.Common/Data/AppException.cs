using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway.Common.Data
{
    public class AppException : Exception
    {
        //public string? ErrorCode { get; set; }
        //public string? ErrorId { get; set; }
        //public string? ErrorCauses { get; set; }
        //public List<string>? ErrorCauses { get; set; }


        //public HttpStatusCode StatusCode { get; set; }
        //public object? ErrorCauses { get; set; }
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        //public AppException(HttpStatusCode statusCode) : base()
        //{
        //    StatusCode = statusCode;
        //}

        //public AppException(HttpStatusCode statusCode, string cause) : base()
        //{
        //    StatusCode = statusCode;
        //    //ErrorCauses = AddErrorCause(cause);
        //    ErrorCauses = AddErrorCause(cause);
        //}

        //public AppException(string errorCause) : base(errorCause)
        //{
        //    ErrorCode = "C000029";
        //    ErrorId = Guid.NewGuid().ToString();
        //    //ErrorCauses = AddErrorCause(errorCause);
        //    ErrorCauses = AddErrorCause(errorCause);
        //}

        //private List<string> AddErrorCause(string error)
        //{
        //    List<string> causes = new List<string>();   

        //    if (!string.IsNullOrEmpty(error))
        //    {
        //        causes.Add(error);
        //    }
        //    return causes;
        //    //return new { resultSet = error};
        //}

        //private List<string> AddErrorCause(List<string> errors)
        //{
        //    List<string> causes = new List<string>();

        //    if (errors != null && errors.Count > 0)
        //    {
        //        causes.AddRange(errors);
        //    }

        //    return causes;
        //}



    }
}
