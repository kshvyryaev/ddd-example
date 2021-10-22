using Jaeger;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;

namespace DddExample.Api.Setups
{
    public static class JaegerSetup
    {
        public static IServiceCollection ConfigureJaeger(this IServiceCollection services, IConfiguration configuration)
        {
            var jaegerOptions = configuration.GetSection(JaegerOptions.SectionKey).Get<JaegerOptions>();

            if (!jaegerOptions.Enabled)
            {
                return services;
            }
            
            services.AddOpenTracing();
            
            services.AddSingleton(serviceProvider =>
            {
                var serviceName = serviceProvider.GetRequiredService<IHostEnvironment>().ApplicationName;
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                
                var tracer = GetTracer(serviceName, loggerFactory, jaegerOptions);
                
                GlobalTracer.Register(tracer);
                
                return tracer;
            });

            return services;
        }
        
        private static ITracer GetTracer(string serviceName, ILoggerFactory loggerFactory, JaegerOptions jaegerOptions)
        {
            Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
                .RegisterSenderFactory<ThriftSenderFactory>();

            var samplerConfiguration = new Configuration.SamplerConfiguration(loggerFactory)
                .WithType(ConstSampler.Type)
                .WithParam(1);

            var sender = new Configuration.SenderConfiguration(loggerFactory)
                .WithAgentHost(jaegerOptions.Host)
                .WithAgentPort(jaegerOptions.Port);

            var reporterConfiguration = new Configuration.ReporterConfiguration(loggerFactory)
                .WithLogSpans(true)
                .WithSender(sender);

            var tracer = new Configuration(serviceName, loggerFactory)
                .WithSampler(samplerConfiguration)
                .WithReporter(reporterConfiguration)
                .GetTracer();

            return tracer;
        }
    }
}