using Microsoft.AspNetCore.Http;

namespace Dzidek.Net.AutoUpgrade.Service;

public interface IAutoUpgradeService
{
    string GetVersion();
    Task Upgrade(IFormFile newLibraryVersion);
}