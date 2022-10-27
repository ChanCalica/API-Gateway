using API_Gateway.Common.Logics.Interface;
using API_Gateway.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private readonly IFileManagerLogic _fileManagerLogic;
        public UploadController(IFileManagerLogic fileManagerLogic)
        {

            _fileManagerLogic = fileManagerLogic;
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] FileModel model)
        {
            if (model.MyFile != null)
            {

                await _fileManagerLogic.Upload(model);

            }
            return Ok();

        }



    }

}
