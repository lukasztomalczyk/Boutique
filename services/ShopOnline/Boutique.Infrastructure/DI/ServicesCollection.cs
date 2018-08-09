using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Boutique.Infrastructure.DDD;
using Boutique.Messages.EventBusRabbitMQ;


namespace Boutique.Infrastructure.DI
{
    public static class ServicesCollection
    {
        public static void AddEventBus(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMqConnection, RabbitMqConnection>();
            services.AddScoped<IEventBus, EventBusRabbitMq>();
        }
        
        public static void AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan =>
            scan.FromAssemblies(assemblies)
            .AddClasses(classess => classess.AssignableTo(typeof(IDomainCommandHandler<>)))
            .AddClasses(classess => classess.AssignableTo(typeof(IDomainCommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            services.AddScoped(p =>
            {
                var sqlConnection = services.BuildServiceProvider().GetRequiredService<SqlConnection>();
                sqlConnection.Open();
                return sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            });
        }

        public static void AddCQRS2(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan =>
               scan.FromAssemblies(assemblies)
                   .AddClasses(classess => classess.AssignableTo(typeof(IDomainEventHandler<>)))
                    .AsImplementedInterfaces()
                   .WithScopedLifetime());
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        }

        public static void AddServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan =>
            scan.FromAssemblies(assemblies)
            .AddClasses(p => p.WithAttribute(typeof(RepositoryAttribute)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.Scan(scan =>
           scan.FromAssemblies(assemblies)
           .AddClasses(p => p.WithAttribute(typeof(ServicesAttribute)))
           .AsImplementedInterfaces()
           .WithScopedLifetime());
        }

        public static void AddAuthJwt(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
        }
    }
}
