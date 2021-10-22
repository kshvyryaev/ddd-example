using DddExample.Api.Infrastructure;
using DddExample.Api.Setups;
using DddExample.Application;
using DddExample.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureSwaggerService();

            services.ConfigureHealthChecks();

            services.ConfigureJaeger(_configuration);

            services.ConfigureApplication(_configuration);

            services.ConfigureInfrastructure(_configuration);
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerService();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks();
            });
        }
    }
}