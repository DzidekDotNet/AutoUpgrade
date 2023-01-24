namespace Dzidek.Net.AutoUpgrade.Service;

public sealed class AutoUpgradeServiceConfiguration
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string NewVersionDirectoryName { get; init; } = "NewVersion";
    public string ServiceNameSuffix { get; init; } = "Service";
    public string ServiceName { get; init;  }

    public AutoUpgradeServiceConfiguration()
    {
        
    }
    public AutoUpgradeServiceConfiguration(string serviceName):this()
    {
        ServiceName = serviceName;
    }

}
