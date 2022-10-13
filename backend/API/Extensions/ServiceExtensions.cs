using API.ActionFilters;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service;
using Service.Contracts;

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
    }
}
