using System.Threading.Tasks;
using hsl.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace hsl.bl.Interfaces
{
    public interface IToken
    {
        Task<IActionResult> RefreshToken(TokenRequestModel model);
        Task<IActionResult> GenerateNewToken(TokenRequestModel model);
    }
}