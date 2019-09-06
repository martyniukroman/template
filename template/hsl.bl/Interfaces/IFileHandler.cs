using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hsl.bl.Interfaces
{
    public interface IFileHandler
    {
        Task<IActionResult> HandleUserProfileImage(IFormFile formFile, string userId);
    }
}