using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway_FunctionApp.Service.Interface
{
    public interface IFunctionAppService
    {
        Task<string?> GetCardId(string attributeId);
        Task ResetCardId(string cardId);
    }
}
