using System;
using System.Linq;
using System.Threading.Tasks;
using hsl.api.Models;
using hsl.bl.Interfaces;
using hsl.db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace hsl.bl.Services
{
    public class ProfileService : IProfile
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly HslapiContext _appDbContext;

        public ProfileService(
            UserManager<AppUser> userManager,
            HslapiContext hslapiContext
        )
        {
            _appDbContext = hslapiContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetUserDataAsync(string userId)
        {
            // FirstOrDefault returns null if user not found 
            // SingleOrDefault throws an exception if user not found 
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return new OkObjectResult(new
            {
                userName = user?.UserName,
                displayName = user?.DisplayName,
                email = user?.Email,
                firstName = user?.FirstName,
                lastName = user?.LastName,
                gender = user?.Gender,
                location = user?.Location,
            });
        }

        public async Task<IActionResult> UpdateUserDataAsync(ProfileViewModel userData)
        {
            try
            {
                var user = _userManager.Users.SingleOrDefault(u => u.Id == userData.UserId);

                if (user == null)
                    return new BadRequestObjectResult(new ErrorViewModel
                    {
                        code = 404,
                        caption = "User not found",
                        tag = "notFoundError"
                    });

                user.Gender = userData.Gender;
                user.Location = userData.Location;
                user.DisplayName = userData.DisplayName;
                user.FirstName = userData.FirstName;
                user.LastName = userData.LastName;

                await _userManager.UpdateAsync(user);

                return new OkObjectResult(userData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}