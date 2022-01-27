using InternetForum.BLL.Interfaces;
using InternetForum.BLL.MapperSettings;
using InternetForum.BLL.Services;
using InternetForum.DAL;
using InternetForum.DAL.Interfaces;
using InternetForum.DAL.Interfaces.RepositoryInterfaces;
using InternetForum.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace InternetForum.WebAPI
{
    public static class ServiceExtentions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, string secretKey, string audience, string issuer)
        {
            services.
                AddAuthorization().
                AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.Audience = audience;
                opt.ClaimsIssuer = issuer;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

                    ValidateLifetime = true,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuer = true,
                    ValidIssuer = issuer

                };
            });
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            services.AddScoped<ICommentReactionRepository, CommentReactionRepository>();
            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAnswerUserRepository, AnswerUserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IReactionService, ReactionService>();

            services.AddScoped<IPostService, PostService>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(conf =>
            {
                conf.AddProfile<UserProfile>();
                conf.AddProfile<PostProfile>();
                conf.AddProfile<CommentProfile>();
                conf.AddProfile<ReactionProfile>();
                conf.AddProfile<QuestionnaireProfile>();
                conf.AddProfile<QuestionProfile>();
                conf.AddProfile<AnswerProfile>();
            });
        }
    }
}
