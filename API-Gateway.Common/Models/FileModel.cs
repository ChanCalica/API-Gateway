using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway.Common.Models
{
    public class FileModel
    {

        public IFormFile MyFile { get; set; }
        
    }
}
