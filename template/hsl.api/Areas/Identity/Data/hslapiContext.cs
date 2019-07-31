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
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<RefreshToken>()
                .HasAlternateKey(c => c.UserId)
                .HasName("refreshToken_UserId");

            builder.Entity<RefreshToken>()
                .HasAlternateKey(c => c.Token)
                .HasName("refreshToken_Token");

            base.OnModelCreating(builder);
        }
    }
}
