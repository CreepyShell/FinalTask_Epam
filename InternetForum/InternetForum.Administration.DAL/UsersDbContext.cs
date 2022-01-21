using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetForum.Administration.DAL
{
    public class UsersDbContext : IdentityDbContext<IdentityUser>
    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole[4]
            {
                new IdentityRole("user"),
                new IdentityRole("administrator"),
                new IdentityRole("banned"),
                new IdentityRole("")
            });
            base.OnModelCreating(builder);
        }
    }
}
