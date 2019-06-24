using hsl.dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace hsl.dal
{
    public class dbContext : DbContext
    {
        public DbSet<Phone> Phones { set; get; }
        public DbSet<User> Users { set; get; }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
