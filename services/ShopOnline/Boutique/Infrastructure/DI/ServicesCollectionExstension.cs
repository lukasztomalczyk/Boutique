using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Boutique.Infrastructure.DI
{
    public static class ServicesCollectionExstension
    {
        public static void AddCommandValidators(this IServiceCollection services,params Assembly[] assemblies)
        {
            services.Scan(p => p.FromAssemblies(assemblies)
            .AddClasses(a => a.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .AsSelf()
            .WithScopedLifetime());
        }
    }
}
