using ConsoleAppFramework;
using Library;
using Library.Conf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

namespace Architect.Cli;

public static class Startup
{
    public static void Logging(ILoggingBuilder logging)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(
                LogEventLevel.Debug,
                "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .CreateLogger();

        logging.AddSerilog(Log.Logger);
    }

    public static void Configuration(IConfigurationBuilder configuration)
    {
        var filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".config",
            "architect",
            "architect.conf"
        );

        configuration.Add(new ConfigurationSource(filePath));
    }

    public static void Services(IConfiguration configuration, IServiceCollection services)
    {
        services.Configure<AppConfig>(configuration);
        services.AddTransient<AppConfig>(x => x.GetRequiredService<IOptions<AppConfig>>().Value);
    }

    public static IHost ConfigureHost(this HostApplicationBuilder builder)
    {
        builder.AddLogging();

        var config = builder.AddConfig();

        return builder.Build();
    }

    private static AppConfig AddConfig(this HostApplicationBuilder builder)
    {
        builder.Configuration.AddConfFile();
        builder.Services.Configure<AppConfig>(builder.Configuration);
        builder.Services.AddTransient<AppConfig>(x => x.GetRequiredService<IOptions<AppConfig>>().Value);

        return builder.Configuration.Get<AppConfig>()!;
    }

    private static void AddLogging(this HostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(
                LogEventLevel.Debug,
                "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .CreateLogger();
    }
}
