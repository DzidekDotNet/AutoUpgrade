using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace AutoUpgradeService.Service.Library;

internal sealed class AutoUpgradeService : IAutoUpgradeService
{
    public string GetVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "";
    }

    public async Task Upgrade(IFormFile newLibraryVersion, HttpContext httpContext)
    {
        string extension = Path.GetExtension(newLibraryVersion.FileName);
        string fileName = newLibraryVersion.FileName.Replace(extension, string.Empty);
        string dirPath = Path.Combine(
            Assembly.GetExecutingAssembly().Location.Replace(newLibraryVersion.FileName, string.Empty), "NewVersion");
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        var filePath = Path.Combine(dirPath, $"{fileName}{extension}");
        await using Stream fileStream = new FileStream(filePath, FileMode.Create);
        await newLibraryVersion.CopyToAsync(fileStream);
    }
}