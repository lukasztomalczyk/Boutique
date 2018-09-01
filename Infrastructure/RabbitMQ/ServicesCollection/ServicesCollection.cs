using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Interface;
using RabbitMQ.Settings;

namespace RabbitMQ.ServicesCollection
{
    public static class ServicesCollection
    {
        public static void AddRabbitMq(this IServiceCollection services)
        {
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped(scope=>
            {
                var settings = scope.GetService<IOptions<EventQueueSettings>>().Value;
                var connectionFactory = scope.GetService<IConnectionFactory>();
                var connection = connectionFactory.CreateConnection(settings.ServerAddress);

                return connection.CreateModel();

            });

            services.AddScoped<IRabbitMQWriteClient, RabbitMQWriteClient>();
            services.AddScoped<IRabbitMQReadClient, RabbitMQReadClient>();
        }
    }
}