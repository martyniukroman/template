//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using hsl.api.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//
//namespace hsl.api.Controllers
//{
//    [Authorize(Policy = "RequireLoggedIn")]
//    [Route("api/[controller]/[action]")]
//    public class ProfileController : Controller
//    {
//        private readonly ClaimsPrincipal _caller;
//        private readonly hslapiContext _appDbContext;
//
//        public ProfileController(
//            hslapiContext appDbContext,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _caller = httpContextAccessor.HttpContext.User;
//            _appDbContext = appDbContext;
//        }
//
//        // GET api/profile/get
//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            var userId = _caller.Claims.Single(c => c.Type == "Id");
//
//       
//
//            return new OkObjectResult(new
//            {
//                Message = "This is secure API and user data!",
//                userId.Value,
//            });
//        }
//    }
//}
