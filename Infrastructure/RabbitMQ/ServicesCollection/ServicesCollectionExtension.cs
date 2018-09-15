using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Interface;
using RabbitMQ.Settings;

namespace RabbitMQ.ServicesCollection
{
    public static class ServicesCollectionExtension
    {
        public static void AddRabbitMq(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classess => classess.AssignableTo<IModel>()).AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddSingleton(scope =>
            {
                var settings = scope.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
                var factory = new ConnectionFactory
                {
                    AutomaticRecoveryEnabled = true,
                    TopologyRecoveryEnabled = false
                };

                if (string.IsNullOrEmpty(settings.Uri))
                {
                    factory.Password = settings.Password;
                    factory.HostName = settings.HostName;
                    factory.Port = settings.Port;
                }
                factory.Uri = new Uri(settings.Uri);

                var connection = factory.CreateConnection();

                return connection.CreateModel();

            });

            services.AddScoped<IRabbitMqWriteClient, RabbitMqWriteClient>();
            services.AddScoped<IRabbitMqReadClient, RabbitMqReadClient>();
        }
    }
}