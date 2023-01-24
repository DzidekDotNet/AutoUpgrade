using System.ServiceProcess;
using Dzidek.Net.AutoUpgrade.Common;
using Dzidek.Net.AutoUpgrade.Upgrader.FileSystemWatchers;
using Microsoft.Extensions.Hosting;

namespace Dzidek.Net.AutoUpgrade.Upgrader;

public sealed class UpgraderService : IHostedService
{
    private readonly IFileWatcher _fileWatcher;
    private readonly AutoUpgradeUpgraderConfiguration _configuration;

    public UpgraderService(IFileWatcher fileWatcher, AutoUpgradeUpgraderConfiguration configuration)
    {
        _fileWatcher = fileWatcher;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        string binPath = _configuration.ServicePath;
        string newVersionPath = Path.Combine(binPath, "NewVersion");
        Upgrade(newVersionPath, binPath);
        _fileWatcher.OnStart(
            new FileSystemWatcherConfiguration()
            {
                Enabled = true,
                Path = newVersionPath
            },
            new FileSystemWatcherActions()
            {
                Created = (sender, args) => { Upgrade(newVersionPath, binPath); }
            });
        return Task.CompletedTask;
    }

    private void Upgrade(string newVersionPath, string binPath)
    {
        Repeat(StopAction);

        UnzipAndCopyFiles(newVersionPath, binPath);

        Repeat(StartAction);
    }

    private string GetServiceName()
    {
        return ServiceName.GetServiceName(_configuration.ServiceName,_configuration.ServiceNameSuffix);
    }

    private void StopAction(TimeSpan wait)
    {
        ServiceController appDriver = new ServiceController(GetServiceName());
        if (appDriver.Status == ServiceControllerStatus.Running)
        {
            appDriver.Stop();
            appDriver.WaitForStatus(ServiceControllerStatus.Stopped, wait);
        }
    }

    private void StartAction(TimeSpan wait)
    {
        ServiceController appDriver = new ServiceController(GetServiceName());
        if (appDriver.Status == ServiceControllerStatus.Stopped)
        {
            appDriver.Start();
            appDriver.WaitForStatus(ServiceControllerStatus.Running, wait);
        }
    }

    private void Repeat(Action<TimeSpan> action)
    {
        TimeSpan time = TimeSpan.FromSeconds(5);
        int i = 5;
        while (i >= 0)
        {
            action(time);
            time *= 2;
            i--;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _fileWatcher.OnStop();
        return Task.CompletedTask;
    }

    private void UnzipAndCopyFiles(string sourcePath, string destPath)
    {
        string[] files = Directory.GetFiles(sourcePath);

        foreach (string s in files)
        {
            string fileName = Path.GetFileName(s);
            string destFile = Path.Combine(destPath, fileName);
            File.Copy(s, destFile, true);
            File.Delete(s);
        }
    }
}