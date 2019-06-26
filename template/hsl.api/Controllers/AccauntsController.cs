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
   //     private readonly IRegistrationInterface _registrationService;
        private readonly IMapper _mapper;
        private readonly hslapiContext _context;

        public AccauntsController(UserManager<User> userManager, IMapper mapper, hslapiContext context) // , RegistrationService registrationService
        {
            this._userManager = userManager;
    //        this._registrationService = registrationService;
            this._mapper = mapper;
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] RegistrationUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest( new { message = "Invalid registration model" });

            var userIdentity = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if(!result.Succeeded) return BadRequest( new { message = $"Error on creating identity model | " + result.ToString()});


            try
            {
                await _context.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location});
                _context.SaveChanges();

                var newUser = _context.Customers.FirstOrDefault(x => x.IdentityId == userIdentity.Id);

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error on working with DataBase" });
            }
        }

    }
}
