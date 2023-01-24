namespace Dzidek.Net.AutoUpgrade.Service;

public interface IAutoUpgradeService
{
    string GetVersion();
    Task Upgrade(byte[] newLibraryVersion, string fileName);
}