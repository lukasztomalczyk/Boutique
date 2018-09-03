using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.Impl;
using RabbitMQ.Interface;
using RabbitMQ.Settings;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQ.ServicesCollection
{
    public static class ServicesCollection
    {
        public static void AddRabbitMq(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IConnectionFactory, ConnectionFactory>();   

            services.AddScoped(scope=>
            {
                var settings = scope.GetService<IOptions<RabbitMqSettings>>().Value.ConnectionSettings;

                var factory = new ConnectionFactory();
                
                factory.UserName = settings.GetEnumerator().Current.User;
                    factory.Password = settings.GetEnumerator().Current.Password;
                    factory.HostName = settings.GetEnumerator().Current.HostAddress;
                    factory.Port = settings.GetEnumerator().Current.Port;
                
                var connection = factory.CreateConnection();

                return connection.CreateModel();

            });

            services.AddScoped<IRabbitMQWriteClient, RabbitMQWriteClient>();
            services.AddScoped<IRabbitMqReadClient, RabbitMQReadClient>();
        }
    }
}