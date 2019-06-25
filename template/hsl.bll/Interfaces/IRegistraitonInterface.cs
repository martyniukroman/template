using System.Threading.Tasks;

namespace hsl.bll.Interfaces
{
    public interface IRegistraitonInterface
    {
        Task<Customer> Registrate(Customer customerModel);
    }
}