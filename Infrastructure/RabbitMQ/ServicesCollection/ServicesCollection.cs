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
    public static class ServicesCollection
    {
        public static void AddRabbitMq(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classess => classess.AssignableTo<IModel>()).AsImplementedInterfaces()
                .WithScopedLifetime());
            
            services.AddScoped(scope=>
            {
                var settings = scope.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
                var factory = new ConnectionFactory();
               factory.UserName = settings.ConnectionSettings.First().User;
                   factory.Password = settings.ConnectionSettings.First().Password;
                factory.HostName = settings.ConnectionSettings.First().HostAddress;
                factory.Port = settings.ConnectionSettings.First().Port;


                
                var connection = factory.CreateConnection();

               return connection.CreateModel();

            });

            services.AddScoped<IRabbitMQWriteClient, RabbitMQWriteClient>();
            services.AddScoped<IRabbitMqReadClient, RabbitMQReadClient>();
        }
    }
}