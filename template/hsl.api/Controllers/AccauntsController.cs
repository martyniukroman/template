using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using hsl.api.Interfaces;
using hsl.api.Models;
using hsl.api.Services;
using hsl.api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hsl.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccauntsController : ControllerBase
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IRegistrationInterface _registrationService;

        public AccauntsController(UserManager<User> userManager, RegistrationService registrationService)
        {
            this._userManager = userManager;
            this._registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            if (!ModelState.IsValid) return BadRequest( new { message = "Invalid registration model" });

            var userIdentity = value;

            var result = await _userManager.CreateAsync(userIdentity);

            if(!result.Succeeded) return BadRequest( new { message = "Error on creating identity model" });

            var dbResult = await _registrationService.Register(userIdentity);
            if (dbResult == null)
            {
                if (!result.Succeeded) return BadRequest(new { message = "Error on working with DataBase" });
            }

            return Ok(dbResult);
        }

    }
}
