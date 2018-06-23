using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Boutique.Infrastructure.DI
{
    public static class ServicesCollection
    {
        public static void AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan =>
            scan.FromAssemblies(assemblies)
            .AddClasses(classess => classess.AssignableTo(typeof(ICommandHandler<>)))
            .AddClasses(classess => classess.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        }

        public static void AddServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan =>
            scan.FromAssemblies(assemblies)
            .AddClasses(p => p.WithAttribute(typeof(RepositoryAttribute)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        }

        public static void AddAuth(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
        }
    }
}
