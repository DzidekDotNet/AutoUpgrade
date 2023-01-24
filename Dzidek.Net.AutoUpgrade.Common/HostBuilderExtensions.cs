using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dzidek.Net.AutoUpgrade.Common;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseAutoUpgrade(this IHostBuilder hostBuilder, string serviceName,
        string serviceNameSuffix, Action<IServiceCollection> configureDelegate)
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            hostBuilder.UseContentRoot(entryAssembly.Location.Replace(entryAssembly.ManifestModule.Name, string.Empty));
        }

        return hostBuilder
            .UseWindowsService(options =>
            {
                options.ServiceName =
                    ServiceName.GetServiceName(serviceName, serviceNameSuffix);
            })
            .ConfigureServices(services =>
            {
                services
                    .AddWindowsService();
                configureDelegate(services);
            });
    }
}