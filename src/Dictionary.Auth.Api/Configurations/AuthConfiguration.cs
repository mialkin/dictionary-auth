using System.Net;
using Dictionary.Auth.Api.Configurations.DataProtection;
using Dictionary.Auth.Controllers.Auth.Constants;
using Dictionary.Auth.Controllers.Settings;
using Microsoft.Extensions.Options;

namespace Dictionary.Auth.Api.Configurations;

public static class AuthConfiguration
{
    public static void ConfigureAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.ConfigureDataProtection(configuration);

        services.AddAuthentication(DefaultAuthenticationScheme.Name)
            .AddCookie(DefaultAuthenticationScheme.Name, options =>
            {
                options.Cookie.Name = "Dictionary.Session";
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                };
            });

        services.AddAuthorization();

        services
            .AddOptions<LogoutSettings>()
            .BindConfiguration(nameof(LogoutSettings))
            .Validate(x =>
            {
                const string optionsName = nameof(LogoutSettings);

                if (string.IsNullOrWhiteSpace(x.RedirectUri))
                    throw new OptionsValidationException(
                        optionsName,
                        optionsType: typeof(string),
                        failureMessages: new[] { $"'{nameof(x.RedirectUri)}' property of '{optionsName}' is empty" });

                return true;
            })
            .ValidateOnStart();
    }
}
