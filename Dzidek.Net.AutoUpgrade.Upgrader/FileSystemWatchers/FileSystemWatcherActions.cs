// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Dzidek.Net.AutoUpgrade.Upgrader.FileSystemWatchers;

public sealed class FileSystemWatcherActions
{
    public FileSystemEventHandler? Changed { get; init; }
    public FileSystemEventHandler? Deleted { get; init; }
    public FileSystemEventHandler? Created { get; init; }
    public RenamedEventHandler? Renamed { get; init; }
}