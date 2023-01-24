using Dzidek.Net.AutoUpgrade.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dzidek.Net.AutoUpgrade.Service;

public static class UseAutoUpgradeServiceRegistration
{
    public static IHostBuilder UseAutoUpgradeService(this IHostBuilder hostBuilder,
        AutoUpgradeServiceConfiguration configuration)
    {
        return hostBuilder.UseAutoUpgrade(configuration.ServiceName, configuration.ServiceNameSuffix, services =>
        {
            services
                .AddSingleton<AutoUpgradeServiceConfiguration>(x => configuration)
                .AddTransient<IAutoUpgradeService, AutoUpgradeService>();
        });
    }
}