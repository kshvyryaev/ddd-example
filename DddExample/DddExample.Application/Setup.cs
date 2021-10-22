using System.Reflection;
using DddExample.Application.Options;
using DddExample.Application.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Application
{
    public static class Setup
    {
        public static IServiceCollection ConfigureApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            // Http context accessor
            services.AddHttpContextAccessor();

            // Memory cache
            services.AddMemoryCache();
            
            // Options
            services.ConfigureOptions(configuration);

            // Commands and domain event handlers
            services.AddMediatR(assembly);

            // Queries
            services.ConfigureQueries(configuration);

            return services;
        }

        private static IServiceCollection ConfigureOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(x => 
                x.ConnectionString = configuration.GetConnectionString("DatabaseContext"));
            
            return services;
        }

        private static IServiceCollection ConfigureQueries(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IBooksQueries, BooksQueries>();

            return services;
        }
    }
}