using API.ActionFilters;
using API.SeedData;
using Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Service.Contracts;
using System;
using System.Text;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("postgreSqlConnection")));
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(MappingProfile));
        public static void ConfigureValidationFilter(this IServiceCollection services) =>
            services.AddScoped<ValidationFilterAttribute>();
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
                };
            });
        }
        public static void ConfigureSeedData(this IServiceCollection services)
        {
            services.AddScoped<IdentityDataSeeder>();
            services.AddHostedService<SetupIdentityDataSeeder>();
        }
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("ShouldBeAdmin", policy =>
                {
                    policy.RequireRole("admin");
                });
            });
        }
    }
}
