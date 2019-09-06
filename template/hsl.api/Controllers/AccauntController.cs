using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hsl.api.Models;
using hsl.db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace hsl.api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccauntController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private JwtIssuerOptions _jwtIssuerOptions;

        public AccauntController(UserManager<AppUser> userManager, IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            this._userManager = userManager;
            this._jwtIssuerOptions = jwtIssuerOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel formData)
        {
            // holds errors related to registration process
            var errors = new List<string>();

            var user = new AppUser()
            {
                Email = formData.Email,
                UserName = formData.DisplayName, //UserName and Display is now the same
                DisplayName = formData.DisplayName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Gender = formData.Gender,
            };

            var result = await _userManager.CreateAsync(user, formData.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return new OkObjectResult(
                    new
                    {
                        userName = user.UserName,
                        email = user.Email,
                        displayName = user.DisplayName,
                        status = 1,
                    }
                );
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                    errors.Add(error.Description);
                }

                return new BadRequestObjectResult(errors);
            }

            //TODO: send confirmation email
        }

        [HttpGet]
        public async Task<IActionResult> IsUserNameExist([FromQuery] string username)
        {
            return new OkObjectResult( await _userManager.FindByNameAsync(username) != null);
        }
        
        [HttpGet]
        public async Task<IActionResult> IsUserEmailExist([FromQuery] string useremail)
        {
            return new OkObjectResult( await _userManager.FindByEmailAsync(useremail) != null);
        }
        
    }
}