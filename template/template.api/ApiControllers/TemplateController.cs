using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using template.api.Helpers;
using template.api.Models;

namespace template.api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        public TemplateController()
        {
        }

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