using hsl.api.Interfaces;
using hsl.api.Models;
using hsl.api.ViewModels;
using Microsoft.AspNetCore.Identity;
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

        public async Task<User> Register(User identityUser)
        {
            try
            {
                _context.Users.Add(identityUser);
                _context.SaveChanges();

                return _context.Users.FirstOrDefault(x => x.Email == identityUser.Email) as User;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
