using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hsl.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hsl.api.Controllers
{
    [Authorize(Policy = "RequireLoggedIn")]
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly hslapiContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public ProfileController(
            hslapiContext appDbContext,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        // GET api/profile/get
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string userName)
        {

            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null) return new BadRequestObjectResult("User not found");
            
            
            return new OkObjectResult(new
            {
                userName = user.UserName,
                displayName = user.DisplayName,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                gender = user.Gender,
                location = user.Location,
            });
        }
    }
}