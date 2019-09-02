using System.Threading.Tasks;
using hsl.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace hsl.bl.Interfaces
{
    public interface IProfile
    {
        Task<IActionResult> GetUserDataAsync(string userId);
        Task<IActionResult> UpdateUserDataAsync(ProfileViewModel userData);
    }
}