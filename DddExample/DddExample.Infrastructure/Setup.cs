using DddExample.Domain.Aggregates;
using DddExample.Domain.Aggregates.BookAggregate;
using DddExample.Infrastructure.Data;
using DddExample.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options
                => options.UseSqlServer(configuration.GetConnectionString(nameof(DatabaseContext))));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBooksRepository, BooksRepository>();

            return services;
        }
    }
}