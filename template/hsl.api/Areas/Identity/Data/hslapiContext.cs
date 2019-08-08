using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hsl.api.Models
{
    public class hslapiContext : IdentityDbContext<User>
    {
        public hslapiContext(DbContextOptions<hslapiContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { set; get; }
        public DbSet<Good> Goods { set; get; }
//        public DbSet<TokenModel> Tokens { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

//            builder.Entity<TokenModel>()
//                .HasAlternateKey(c => c.UserId)
//                .HasName("refreshToken_UserId");
//
//            builder.Entity<TokenModel>()
//                .HasAlternateKey(c => c.Token)
//                .HasName("refreshToken_Token");

            base.OnModelCreating(builder);
        }
    }
}
