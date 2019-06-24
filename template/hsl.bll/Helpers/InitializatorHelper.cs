using hsl.api.Models;
using System.Collections.Generic;
using System.Linq;

namespace template.api.Helpers
{
    public static class InitializatorHelper
    {
        public static IEnumerable<User> GetUsers()
        {

            var phones = GetPhones().ToList();
            
            var tmpUsers = new List<User>
            {
                new User {Id = 1, Name = "Vasyan", Age = 18, PhoneId = 3, UserPhone = phones[2]},
                new User {Id = 2, Name = "Stasyan", Age = 32, PhoneId = 2, UserPhone = phones[1]},
                new User {Id = 3, Name = "Kolan", Age = 40, PhoneId = 1, UserPhone = phones[0]}
            };

            return tmpUsers;
        }
        
        public static IEnumerable<Phone> GetPhones()
        {

            var tmpUsers = new List<Phone>
            {
                new Phone {Id = 1, Brand = "Apple", Model = "Iphone 6s"},
                new Phone {Id = 2, Brand = "Google", Model = "Pixel"},
                new Phone {Id = 3, Brand = "Samsung", Model = "Galaxy s8"}
            };

            return tmpUsers;
        }
        
    }
}