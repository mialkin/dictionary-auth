using Dictionary.Auth.UseCases.Auth;
using Microsoft.Extensions.Options;

namespace Dictionary.Auth.Api.Settings;

public static class AdminSettingsConfiguration
{
    public static void ConfigureAdminSettings(this IServiceCollection services)
    {
        services
            .AddOptions<AdminSettings>()
            .BindConfiguration(nameof(AdminSettings))
            .Validate(x =>
            {
                var failureMessages = new List<string>();

                if (string.IsNullOrWhiteSpace(x.Username))
                    failureMessages.Add($"'{nameof(x.Username)}' property is empty");

                if (string.IsNullOrWhiteSpace(x.Password))
                    failureMessages.Add($"'{nameof(x.Password)}' property is empty");

                if (failureMessages.Count != 0)
                    throw new OptionsValidationException(
                        optionsName: nameof(AdminSettings),
                        optionsType: typeof(AdminSettings),
                        failureMessages
                    );

                return true;
            })
            .ValidateOnStart();
    }
}
