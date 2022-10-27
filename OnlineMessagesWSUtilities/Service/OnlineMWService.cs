using API_Gateway.Common.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineMessagesService;
using OnlineMessagesWSUtilities.Model;
using OnlineMessagesWSUtilities.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineMessagesWSUtilities.Service
{
    public class OnlineMWService : IOnlineMWService
    {
        private OnlineMessagesServiceSoap? _onlineMessageClient;

        static IConfiguration config = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());

        private static string MSG3URL = config["MSG3WSURL:MSG3"].ToString();

        BasicHttpBinding binding = new BasicHttpBinding();
        EndpointAddress address = new EndpointAddress(MSG3URL);

        private OnlineMessagesServiceSoap OnlineMessageClient
        {
            get
            {
                if (_onlineMessageClient == null)
                {
                    _onlineMessageClient = new OnlineMessagesServiceSoapClient(binding, address);
                }

                return _onlineMessageClient;
            }
        }

        public async Task<RequestDataResponseBody> RequestDataMessages(string requestData, string chain, string branch)
        {
            try
            {
                var request = new RequestDataRequest();
                request.Body = new RequestDataRequestBody()
                {
                    messageHandlerApplication = "UploadOnlineSynchronized",
                    chain = chain,
                    branch = branch,
                    messageData = requestData
                };

                var response = await OnlineMessageClient.RequestDataAsync(request);

                ValidateErrorResponse(response.Body.RequestDataResult);

                return response.Body;
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

        private void ValidateErrorResponse(string xmlText)
        {
            bool success = true;
            string jsonResponse = XMLtoJSON(xmlText);

            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };

            var result = JsonConvert.DeserializeObject<ErrorMSG3>(jsonResponse, settings);

            if (result != null && result.Error != null && !string.IsNullOrEmpty(result.Error.ErrorDescription))
            {
                throw new ErrorException(HttpStatusCode.BadRequest, result.Error.ErrorDescription, "MSG3 Error");
            }
        }

        private static string XMLtoJSON(string xmlText)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(xmlText);

            string jsonText = JsonConvert.SerializeXmlNode(xml);

            return jsonText;
        }
    }
}
