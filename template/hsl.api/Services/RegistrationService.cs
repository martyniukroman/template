using hsl.api.Interfaces;
using hsl.api.Models;
using hsl.api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hsl.api.Services
{
    public class RegistrationService : IRegistrationInterface
    {
        private readonly hslapiContext _context;

        public RegistrationService(hslapiContext context)
        {
            this._context = context;
        }

        public async Task<Customer> Register(User identityUser, RegistrationUserViewModel model)
        {
            try
            {
                await _context.Customers.AddAsync( new Customer { IdentityId = identityUser.Id, Location = model.Location });
                _context.SaveChanges();

                return _context.Customers.FirstOrDefault(x => x.IdentityId == identityUser.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
