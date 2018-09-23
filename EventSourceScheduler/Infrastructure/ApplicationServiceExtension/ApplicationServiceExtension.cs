using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Interface;
using Cqrs.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EventSourceScheduler.Infrastructure.PortAdapters;
using System.Threading;
using RabbitMQ.Settings;

namespace EventSourceScheduler.Infrastructure.ApplicationServiceExtension
{
    public static class ApplicationServiceExtension
    {
        public static void UseEventSourceListener(this IApplicationBuilder app, CancellationToken cancelletionToken)
        {
            var provider = app.ApplicationServices.GetService<IServiceProvider>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var settings = app.ApplicationServices.GetRequiredService<IOptions<RabbitMqSettings>>();

            using (var scope = provider.CreateScope())
            {
                var rabbitReadClient = scope.ServiceProvider.GetRequiredService<IRabbitMqReadClient>();
                var listenerHandler = scope.ServiceProvider.GetRequiredService<IListenerHandler>();

                var eventListener = new EventListener(logger, rabbitReadClient, settings, listenerHandler);

                eventListener.Listen(cancelletionToken);
            }

        }
    }
}
