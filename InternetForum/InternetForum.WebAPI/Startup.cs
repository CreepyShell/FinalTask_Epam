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
using InternetForum.BLL.Helpers;
using InternetForum.Administration.DAL.IdentityModels;
using System;
using Serilog;

namespace InternetForum.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddLogging(conf => conf.AddSerilog(dispose: true));
            services.AddControllers();

            services.AddDbContext<IForumDb, ForumDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ForumDbDomainConnection")));

            services.AddDbContext<UsersDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("UserManagerConnection")));

            
            services.AddIdentity<AuthUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddTokenProvider("Provider", typeof(DataProtectorTokenProvider<AuthUser>));

            services.Configure<DataProtectionTokenProviderOptions>(opt => 
                { opt.TokenLifespan = TimeSpan.FromDays(Convert.ToDouble(Configuration.GetSection("RefreshTokenExpirationDays").Value)); });

            JwtSettings settings = Configuration.GetSection("JwtSection").Get<JwtSettings>();

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSection"));
            services.RegisterRepositories();
            services.RegisterAutoMapper();
            services.RegisterServices();

            services.AddJwtAuthentication(settings.JwtKey, settings.Audience, settings.Issuer);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(conf =>
            {
                conf.AllowAnyMethod();
                conf.WithOrigins("http://localhost:4200");
                conf.WithOrigins("https://poseidno228.github.io");
                conf.WithOrigins("https://web.postman.co/");
                conf.AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
