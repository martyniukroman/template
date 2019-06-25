using System;
using System.Linq;
using System.Threading.Tasks;
using hsl.bll.Interfaces;
using hsl.dto.Models;

namespace hsl.bll.Services
{
    public class RegistrationService : IRegistraitonInterface
    {

        private readonly hslapiContext _context;

        public RegistrationService(hslapiContext context)
        {
            this._context = context;
        }

        public async Task<Customer> Registrate(Customer customerIdentityModel)
        {
            try
            {
                await _context.Customers.AddAsync( new Customer { IdentityId = customerIdentityModel.Id, Location = customerIdentityModel.Location });
                await _context.SaveChangesAsync();

                return _context.Customers.ToList().FirstOrDefault(x => x.Id == customerIdentityModel.Id);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}