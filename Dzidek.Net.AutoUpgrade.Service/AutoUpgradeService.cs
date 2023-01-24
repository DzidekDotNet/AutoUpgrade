using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Dzidek.Net.AutoUpgrade.Service;

internal sealed class AutoUpgradeService : IAutoUpgradeService
{
    private readonly AutoUpgradeServiceConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public AutoUpgradeService(AutoUpgradeServiceConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }
    public string GetVersion()
    {
        return Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "";
    }

    public async Task Upgrade(IFormFile newLibraryVersion)
    {
        string dirPath = Path.Combine(_env.ContentRootPath, _configuration.NewVersionDirectoryName);
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        await using Stream fileStream = new FileStream(Path.Combine(dirPath, newLibraryVersion.FileName), FileMode.Create);
        await newLibraryVersion.CopyToAsync(fileStream);
    }
}