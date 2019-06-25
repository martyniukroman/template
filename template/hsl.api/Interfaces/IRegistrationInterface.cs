using hsl.api.Models;
using hsl.api.ViewModels;
using System.Threading.Tasks;

namespace hsl.api.Interfaces
{
    public interface IRegistrationInterface
    {
       Task<Customer> Register(User identityUser, RegistrationUserViewModel model);
    }
}
