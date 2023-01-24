using Microsoft.AspNetCore.Http;

namespace AutoUpgradeService.Service.Library;

public interface IAutoUpgradeService
{
    string GetVersion();
    Task Upgrade(IFormFile newLibraryVersion, HttpContext httpContext);
}