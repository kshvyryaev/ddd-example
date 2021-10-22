using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DddExample.Api.Setups
{
    public static class SwaggerSetup
    {
        public static IServiceCollection ConfigureSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("latest", new OpenApiInfo
                {
                    Version = "latest",
                    Title = "DddExample.Api"
                });

                #region Documentation

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                #endregion Documentation
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("latest/swagger.json", "DddExample.Api"));
            
            return app;
        }
    }
}