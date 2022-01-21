using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetForum.Administration.DAL
{
    public class UsersDbContext : IdentityDbContext<AuthUser>
    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SeedData();
            base.OnModelCreating(builder);
        }
    }
}
