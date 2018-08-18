using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Interface;

namespace RabbitMQ.ServicesCollection
{
    public static class ServicesCollection
    {
        public static void AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IEventBusServices, EventBusServices>();
            services.AddScoped<IConnectionEventBus, ConnectionEventBus>();
        }
    }
}