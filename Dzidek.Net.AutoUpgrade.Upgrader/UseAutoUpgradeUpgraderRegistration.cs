using Dzidek.Net.AutoUpgrade.Common;
using Dzidek.Net.AutoUpgrade.Upgrader.FileSystemWatchers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dzidek.Net.AutoUpgrade.Upgrader;

public static class UseAutoUpgradeUpgraderRegistration
{
    public static IHostBuilder UseAutoUpgradeUpgrader(this IHostBuilder hostBuilder,
        AutoUpgradeUpgraderConfiguration configuration)
    {
        return hostBuilder.UseAutoUpgrade(configuration.ServiceName, configuration.UpgraderNameSuffix, services =>
        {
            services
                .AddSingleton<IFileWatcher, FileWatcher>()
                .AddSingleton<AutoUpgradeUpgraderConfiguration>(x => configuration)
                .AddHostedService<UpgraderService>();
        });
    }
}