using Dictionary.Auth.Api.Settings;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

namespace Dictionary.Auth.Api.Configurations;

public static class DataProtectionConfiguration
{
    public static void ConfigureDataProtection(this IServiceCollection services, ConfigurationManager configuration)
    {
        var dataProtectionSettings = new DataProtectionSettings();
        configuration.GetRequiredSection(nameof(DataProtectionSettings)).Bind(dataProtectionSettings);

        if (string.IsNullOrWhiteSpace(dataProtectionSettings.ApplicationName))
            throw new InvalidOperationException(
                $"'{nameof(DataProtectionSettings.ApplicationName)}' setting is not set");

        if (string.IsNullOrWhiteSpace(dataProtectionSettings.RedisConfiguration))
            throw new InvalidOperationException(
                $"'{nameof(DataProtectionSettings.RedisConfiguration)}' setting is not set");

        services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(dataProtectionSettings.RedisConfiguration))
            .SetApplicationName(dataProtectionSettings.ApplicationName);
    }
}
