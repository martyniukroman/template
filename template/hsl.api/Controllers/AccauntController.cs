using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hsl.api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace hsl.api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccauntController : ControllerBase
    {
        private UserManager<User> _userManager;
//        private SignInManager<User> _signInManager;
        private JwtIssuerOptions _jwtIssuerOptions;

        public AccauntController(UserManager<User> userManager, IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            this._userManager = userManager;
//            this._signInManager = signInManager;
            this._jwtIssuerOptions = jwtIssuerOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel formData)
        {
            // holds errors related to registration process
            var errors = new List<string>();

            var user = new User()
            {
                Email = formData.Email,
                UserName = formData.Email,
                DisplayName = formData.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Gender = formData.Gender,
                Location = formData.Location,
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
        
    }
}