using Microsoft.Extensions.DependencyInjection;

namespace AutoUpgradeService.Service.Library;

public static class UseLibraryRegistration
{
    public static IServiceCollection AddLibrary(this IServiceCollection services)
    {
        return services
            .AddTransient<IAutoUpgradeService, AutoUpgradeService>();
    }
}