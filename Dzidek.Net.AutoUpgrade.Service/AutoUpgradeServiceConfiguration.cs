namespace Dzidek.Net.AutoUpgrade.Service;

public sealed class AutoUpgradeServiceConfiguration
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string NewVersionDirectoryName { get; init; } = "NewVersion";
    public string ServiceNameSuffix { get; init; } = "Service";
    public string ServiceName { get; }

    public AutoUpgradeServiceConfiguration(string serviceName)
    {
        ServiceName = serviceName;
    }
}
