using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using hsl.api.Models;
using hsl.bl.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hsl.api.Controllers
{
    [Authorize(Policy = "RequireLoggedIn")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly IFileHandler _fileHandler;
        
        public FileController(IFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateUserImage([FromQuery] string userId)
        {
            try
            {
                var file = Request.Form.Files[0];
                return await this._fileHandler.HandleUserProfileImage(file, userId);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ErrorViewModel
                {
                    code = 500,
                    caption = ex.Message + ' ' + ex.InnerException?.Message,
                    tag = "exceptionError"
                });
            }
        }
    }
}