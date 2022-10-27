using OnlineMessagesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMessagesWSUtilities.Service.Interface
{
    public interface IOnlineMWService
    {
        Task<RequestDataResponseBody> RequestDataMessages(string requestData, string chain, string branch);
    }
}
