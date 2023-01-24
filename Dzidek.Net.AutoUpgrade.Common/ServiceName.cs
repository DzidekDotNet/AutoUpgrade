namespace Dzidek.Net.AutoUpgrade.Common;

public static class ServiceName
{
    public static string GetServiceName(string serviceName, string suffix)
    {
        return $"{serviceName}.{suffix}";
    }
}