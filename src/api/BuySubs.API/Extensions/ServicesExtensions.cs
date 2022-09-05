using BuySubs.BLL.Commands.Auth;
using Riok.Mapperly.Abstractions;

namespace BuySubs.API.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.Scan(q => q
            .FromAssemblyOf<SignUpCommand>()
                .AddClasses(q => q.WithAttribute<MapperAttribute>())
                .AsSelf()
                .WithSingletonLifetime()
        );

        return services;
    }
}