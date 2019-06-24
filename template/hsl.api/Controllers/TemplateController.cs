using System.Linq;
using hsl.api.Models;
using Microsoft.AspNetCore.Mvc;
using template.api.Helpers;
using hsl.dal;
using Microsoft.EntityFrameworkCore;

namespace template.api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {

        // THERE IS A NEED OF DATABASE SQL SERVER BECAUSE WHILE INTIALIZATION OF DBCONTEXT THE ERROR IS THROWN IN CASE OF MISSING SQL SERVER

        //private readonly dbContext _context;

        //public TemplateController(dbContext db)
        //{
        //    this._context = db;
        //}

        //[HttpPost("entity_post_user")]
        //public IActionResult GetUsers([FromBody] hsl.dal.Entities.User user)
        //{
        //    var inserted = _context.Users.Add(user);
        //    _context.SaveChanges();

        //    return inserted == null ? (IActionResult)BadRequest(new { error = "Server error" }) : Ok(inserted);
        //}
        //[HttpGet("entity_get_users")]
        //public IActionResult GetUsersdb()
        //{
        //    var users = _context.Users.ToList();
        //    return users == null ? (IActionResult)BadRequest(new { error = "Server error" }) : Ok(users);
        //}
        [HttpGet("get_users")]
        public IActionResult GetUsers()
        {
            var users = InitializatorHelper.GetUsers().ToList();
            return users == null ? (IActionResult)BadRequest(new { error = "Server error" }) : Ok(users);
        }
        [HttpGet("get_user_by_id")]
        public IActionResult GetUsers([FromQuery]int id)
        {
            var user = InitializatorHelper.GetUsers().FirstOrDefault( x => x.Id == id);
            return user == null ? (IActionResult)BadRequest(new { error = "User out of range" }) : Ok(user);
        }
        [HttpGet("get_users_with_phones")]
        public IActionResult GetUsersWithPhones()
        {
            var usersWithPhones = InitializatorHelper.GetUsers().Select( x =>
                new
                {
                    id = x.Id,
                    name = x.Name,
                    age = x.Age,
                    
                    phoneId = x.UserPhone.Id, 
                    phoneBrand = x.UserPhone.Brand, 
                    phoneModel = x.UserPhone.Model 
                }
            );
            
            return usersWithPhones == null ? (IActionResult)BadRequest(new { error = "Server error" }) : Ok(usersWithPhones);
        }
    }
}