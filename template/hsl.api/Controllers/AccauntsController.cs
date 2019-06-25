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
        private readonly IMapper _mapper;

        public AccauntsController(UserManager<User> userManager, RegistrationService registrationService, IMapper mapper)
        {
            this._userManager = userManager;
            this._registrationService = registrationService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest( new { message = "Invalid registration model" });

            var userIdentity = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if(!result.Succeeded) return BadRequest( new { message = "Error on creating identity model" });

            var dbResult = await _registrationService.Register(userIdentity, model);
            if (dbResult == null)
            {
                if (!result.Succeeded) return BadRequest(new { message = "Error on working with DataBase" });
            }

            return Ok(dbResult);
        }

    }
}
