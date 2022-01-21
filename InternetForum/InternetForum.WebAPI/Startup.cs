using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using InternetForum.DAL;
using InternetForum.DAL.Interfaces;
using InternetForum.Administration.DAL;
using Microsoft.AspNetCore.Identity;

namespace InternetForum.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<IForumDb, ForumDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ForumDbDomainConnection")));

            services.AddDbContext<UsersDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("UserManagerConnection")));

            services.AddIdentity<AuthUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
            })
                .AddEntityFrameworkStores<UsersDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
