using System.Reflection;

namespace Dzidek.Net.AutoUpgrade.Service;

internal sealed class AutoUpgradeService : IAutoUpgradeService
{
    private readonly AutoUpgradeServiceConfiguration _configuration;

    public AutoUpgradeService(AutoUpgradeServiceConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetVersion()
    {
        return Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "";
    }

    public async Task Upgrade(byte[] newLibraryVersion, string fileName)
    {
        string dirPath = Path.Combine(_configuration.NewVersionDirectoryName);
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        if (Directory.GetFiles(dirPath).Any())
        {
            throw new UpgradePackageNotInstalledException(dirPath);
        }

        await File.WriteAllBytesAsync(Path.Combine(dirPath, fileName), newLibraryVersion);
    }
}