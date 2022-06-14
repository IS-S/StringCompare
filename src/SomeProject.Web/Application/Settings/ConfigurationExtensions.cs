using System.IO;
using Microsoft.Extensions.Configuration;
using StackExchange.Utils;

namespace SomeProject.Web.Application.Settings;

public static class ConfigurationExtensions
{
    public static  IConfigurationBuilder ConfigureDynamicSettings(this IConfigurationBuilder builder)
    {
        return builder.WithSubstitution(
                c => c.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddConfigDirectory("config", true, true)
            )
            .WithPrefix("secrets", c => c.AddEnvironmentVariables());
    }

    private static void AddConfigDirectory(
        this IConfigurationBuilder builder,
        string directory = "config",
        bool optional = false,
        bool reloadOnChange = false)
    {
        if (!Directory.Exists(directory) && optional)
        {
            return;
        }

        foreach (string configFile in Directory.GetFiles(directory, "*.json"))
        {
            builder.AddJsonFile(configFile, optional, reloadOnChange);
        }
    }
}
