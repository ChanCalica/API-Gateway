using API_Gateway.Common.Data;
using API_Gateway.Common.Logics;
using API_Gateway.Common.Logics.Interface;
using API_Gateway.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_Gateway.Common.ExceptionHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IFileManagerLogic fileManagerLogic)
        {
            try
            {
                await _next(context);
            }
            catch (ErrorException ex)
            {

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)ex.StatusCode;

                string correlationId = GetCorrelationId(context.Request);

                ex.Type = string.IsNullOrEmpty(ex.Type) ? "logic" : ex.Type;

                var errorDetails = new ErrorResponse()
                {
                    ErrorCode = "E000026",
                    ErrorId = Guid.NewGuid().ToString(),
                    ErrorCauses = ex.ErrorCauses,
                    Type = ex.Type,
                    CurrentURL = ex.CurrentURL,
                    Summary = ex.Summary,
                    CorrelationId = correlationId
                };

                var Errordetails = new ErrorsResponse()
                {
                    ErrorId = errorDetails.ErrorId,
                    ErrorCauses = ex.ErrorCauses,
                };

                var result = JsonSerializer.Serialize(errorDetails);
                var Result = JsonSerializer.Serialize(Errordetails);

                string fileName = errorDetails.ErrorId + "-" + correlationId + "-" + ex.Type.ToLower() + "-" + DateTime.Now.ToString("yyyyMMddHHmm") +".txt";

                await fileManagerLogic.UploadBlob(fileName, result);

                await response.WriteAsync(Result);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                string correlationId = GetCorrelationId(context.Request);

                var errorDetails = new ErrorResponse()
                {
                    ErrorCode = "E000026",
                    ErrorId = Guid.NewGuid().ToString(),
                    ErrorCauses = new List<string>(),
                    CorrelationId = correlationId,
                    Type = "Logic (Unhandled)"
                };

                errorDetails.ErrorCauses.Add(ex.Message);

                var Errordetails = new ErrorsResponse()
                {
                    ErrorId = errorDetails.ErrorId,
                    ErrorCauses = errorDetails.ErrorCauses,
                };

                var result = JsonSerializer.Serialize(errorDetails);
                var Result = JsonSerializer.Serialize(Errordetails);

                string fileName = errorDetails.ErrorId + "-" + correlationId + "-" + "logic" + "-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

                await fileManagerLogic.UploadBlob(fileName, result);

                await response.WriteAsync(Result);
            }
        }

        private string GetCorrelationId(HttpRequest request)
        {
            string id = string.Empty;

            if (request != null)
            {
                bool hasCID = request.Query.ContainsKey("cid");
                if (hasCID)
                {
                    id = request.Query["cid"];
                }
            }

            return id;
        }
    }
}
