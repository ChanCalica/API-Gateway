using API_Gateway.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway.Common.Logics.Interface
{
    public interface IFileManagerLogic
    {

        Task Upload(FileModel model);
        Task UploadBlob(string fileName, string error);
        Task MemberUpdateUploadBlob(string fileName, string result);
        Task ActivityLogUploadBlob(string fileName, string result);

        
    }
}
