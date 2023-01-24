using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dzidek.Net.AutoUpgrade.Common;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseAutoUpgrade(this IHostBuilder hostBuilder, string serviceName,
        string serviceNameSuffix, Action<IServiceCollection> configureDelegate)
    {
        return hostBuilder
            .UseWindowsService(options =>
            {
                options.ServiceName =
                    ServiceName.GetServiceName(serviceName, serviceNameSuffix);
            })
            .ConfigureServices(configureDelegate);
    }
}