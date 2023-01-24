namespace Dzidek.Net.AutoUpgrade.Upgrader.FileSystemWatchers;

public class FileSystemWatcherConfiguration
{
    /// <summary>
    /// Folder will be monitored only when TRUE
    /// Default: false
    /// </summary>
    public bool Enabled { get; init; }
    /// <summary>
    /// File filter
    /// Example: .txt, *zip
    /// </summary>
    public string? FileFilter { get; init; }
    /// <summary>
    /// Full path to folder
    /// </summary>
    public string Path { get; init; }
    /// <summary>
    /// If true the folder from path and its subfolders will be monitored
    /// Default: false
    /// </summary>
    public bool IncludeSubfolders { get; init; }
}