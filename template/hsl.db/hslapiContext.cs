using hsl.db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hsl.api.Models
{
    public class HslapiContext : IdentityDbContext<AppUser>
    {
        public HslapiContext(DbContextOptions<HslapiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { set; get; }
        public DbSet<AppProductImage> AppProductImages { set; get; }
        public DbSet<AppUserImage> AppUserImages { set; get; }
        public DbSet<RefreshTokenModel> Tokens { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" },
                new { Id = "3", Name = "Moderator", NormalizedName = "MODERATOR" }
            );
        }
    }
}
