namespace Dzidek.Net.AutoUpgrade.Upgrader.FileSystemWatchers;

public class FileWatcher : IFileWatcher
{
    private readonly FileSystemWatcher _fileSystemWatcher = new FileSystemWatcher();
    
    public void OnStart(FileSystemWatcherConfiguration configuration, FileSystemWatcherActions actions)
    {
        if (configuration.Enabled)
        {
            if (configuration.FileFilter != null)
            {
                _fileSystemWatcher.Filter = configuration.FileFilter;
            }

            if (!Directory.Exists(configuration.Path))
            {
                Directory.CreateDirectory(configuration.Path);
            }
            _fileSystemWatcher.Path = configuration.Path;
            _fileSystemWatcher.Changed += actions.Changed;
            _fileSystemWatcher.Deleted += actions.Deleted;
            _fileSystemWatcher.Created += actions.Created;
            _fileSystemWatcher.Renamed += actions.Renamed;

            _fileSystemWatcher.EnableRaisingEvents = true;
        }
    }

    public void OnStop()
    {
        _fileSystemWatcher.EnableRaisingEvents = false;
        _fileSystemWatcher.Dispose();
    }
}