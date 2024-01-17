using Dictionary.Auth.UseCases.Auth.Commands;

namespace Dictionary.Auth.Api.Configurations;

public static class MediatrConfiguration
{
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<LoginCommand>());

        return services;
    }
}
