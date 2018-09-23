using Cqrs.Interface;
using EventSourceScheduler.Application.Handlers;
using EventSourceScheduler.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourceScheduler.Infrastructure.DI
{
    public static class ServicesCollectionExtension
    {
        public static void AddSchedulerServices(this IServiceCollection services)
        {
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IListenerHandler, EventSoruceHandler>();
        }
    }
}