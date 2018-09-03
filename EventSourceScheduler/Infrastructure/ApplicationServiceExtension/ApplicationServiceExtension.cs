using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Interface;
using Cqrs.Interface;
using Microsoft.Extensions.Logging;
using EventSourceScheduler.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using EventSourceScheduler.Infrastructure.PortAdapters;
using System.Threading;

namespace EventSourceScheduler.Infrastructure.ApplicationServiceExtension
{
    public static class ApplicationServiceExtension
    {
        public static void UseEventSourceListener(this IApplicationBuilder app, CancellationToken cancelletionToken)
        {
            var provider = app.ApplicationServices.GetService<IServiceProvider>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var settings = app.ApplicationServices.GetRequiredService<IOptions<EventSourceSettings>>();

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
