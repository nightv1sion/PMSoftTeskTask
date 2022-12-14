using API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();
            services.ConfigurePostgreSqlContext(Configuration);
            services.ConfigureMapper();
            services.ConfigureValidationFilter();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
            services.ConfigureSeedData();
            services.ConfigureAuthorization();
            services.ConfigureCORS();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.ConfigureExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }


            app.UseHttpsRedirection();


            app.UseAuthentication();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("DefaultPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
             using(var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
                context.Database.Migrate();
            }
        }
    }
}
