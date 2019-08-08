using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using hsl.api.Models;
using hsl.api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hsl.api.Controllers
{
    [Authorize(Policy = "ApiUser")]
//    [Authorize(Roles = "admin")]
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly hslapiContext _appDbContext;

        public ProfileController(
            hslapiContext appDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/profile/get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");

            // fresh and flesh
            var user = _appDbContext.Users.Join(_appDbContext.Customers,
                x => x.Id,
                y => y.IdentityId,
                (x, y) => new
                {
                    id = x.Id,
                    email = x.UserName,
                    fName = x.FirstName,
                    lName = x.LastName,
                    location = y.Location,
                    gender = y.Gender,
                })
                .SingleOrDefault( x => x.id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                userId.Value,
                user
            });
        }
    }
}
