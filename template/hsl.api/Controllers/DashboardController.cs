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
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly hslapiContext _appDbContext;

        public DashboardController(UserManager<User> userManager, hslapiContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
//            var userId = _caller.Claims.Single(c => c.Type == "id");
//            var customer = await _appDbContext.Customers.Include(c => c.IdentityUser).SingleAsync(c => c.IdentityUser.Id == userId.Value);
      
            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
//                customer.IdentityUser.FirstName,
//                customer.IdentityUser.LastName,
//                customer.IdentityUser.PictureUrl,
//                customer.IdentityUser.FacebookId,
//                customer.Location,
//                customer.Locale,
//                customer.Gender
            });
        }
    }
}