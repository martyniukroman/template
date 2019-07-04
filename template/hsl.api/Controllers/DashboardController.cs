using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hsl.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hsl.api.Controllers
{
    [Authorize(Policy = "User")]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly hslapiContext _context;
        private readonly ClaimsPrincipal _caller;

        public DashboardController(UserManager<User> userManager, hslapiContext hslapiContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = hslapiContext;
            _caller = httpContextAccessor.HttpContext.User;
        }


        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            try
            {
                var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
                var customer = await _context.Customers.Include(x => x.IdentityUser)
                    .SingleAsync(x => x.IdentityUser.Id == userId.Value);

                return new OkObjectResult(customer);

            }
            catch (Exception e)
            {
                return BadRequest(new {message = "Core Error", error = e.Message + e.Source});
            }
        }
    }
}