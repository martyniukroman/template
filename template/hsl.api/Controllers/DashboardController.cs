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
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly hslapiContext _appDbContext;

        public DashboardController(
            hslapiContext appDbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var user = (from c in _appDbContext.Customers
                join u in _appDbContext.Users on c.IdentityId equals u.Id
                where c.IdentityId == userId.Value
                      select new
                      {
                           id = u.Id,
                           mail = u.Email,
                           fName = u.FirstName,
                           LName = u.FirstName,
                           location = c.Location,
                      });
            
            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                userId.Value,
                user
            });
        }
    }
}