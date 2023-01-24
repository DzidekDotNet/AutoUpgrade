namespace Dzidek.Net.AutoUpgrade.Upgrader.FileSystemWatchers;

public interface IFileWatcher
{
    public void OnStart(FileSystemWatcherConfiguration configuration, FileSystemWatcherActions actions);
    public void OnStop();
}