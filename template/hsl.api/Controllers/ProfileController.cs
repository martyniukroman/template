using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using hsl.api.Models;
using hsl.bl.Interfaces;
using hsl.bl.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace hsl.api.Controllers
{
    [Authorize(Policy = "RequireLoggedIn")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ClaimsPrincipal _caller;
        private readonly IProfile _profileService;

        public ProfileController(
            IHttpContextAccessor httpContextAccessor,
            IProfile profileService
        )
        {
            _caller = httpContextAccessor.HttpContext.User;
            _profileService = profileService;
        }

        // GET api/profile/get
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string userId)
        {
            try
            {
                return await _profileService.GetUserDataAsync(userId) ?? new BadRequestObjectResult(new ErrorViewModel
                {
                    code = 404,
                    caption = "User not found",
                    tag = "notFoundError"
                });
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProfileViewModel data)
        {
            try
            {
                return await _profileService.UpdateUserDataAsync(data);
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
        

        [HttpPost]
        public IActionResult UpdateImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var sb = "";

                if (file.Length > 0)
                {
                    
                }

                return Ok();
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