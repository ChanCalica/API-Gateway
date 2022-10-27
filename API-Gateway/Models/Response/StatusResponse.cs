using System.Net;

namespace API_Gateway.Models.Response
{
    public class StatusResponse
    {
        public string ResponseCode { get; set; }
       
        public StatusResponse()
        {
            //StatusCode = "Ok";
            ResponseCode = "201 Created";
        }

    }

    public class SuccessResponse
    {
        public string ResponseCode { get; set; }

        public SuccessResponse()
        {
            //StatusCode = "Ok";
            ResponseCode = "200 Success";
        }


    }


}
