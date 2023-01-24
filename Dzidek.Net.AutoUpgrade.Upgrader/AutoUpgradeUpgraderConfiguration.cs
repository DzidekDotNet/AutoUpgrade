namespace Dzidek.Net.AutoUpgrade.Upgrader;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class AutoUpgradeUpgraderConfiguration
{
    public string ServiceName { get; }
    public string ServicePath { get; }
    public string UpgraderNameSuffix { get; init; } = "Upgrader";
    public string ServiceNameSuffix { get; init; } = "Service";

    public AutoUpgradeUpgraderConfiguration(string serviceName, string servicePath)
    {
        ServiceName = serviceName;
        ServicePath = servicePath;
    }
}
