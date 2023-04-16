
# Windows service with auto upgrade feature

An auto-updating Windows service with the ability to send a new compressed version on demand
![C1 and sequence diagram](https://github.com/DzidekDotNet/AutoUpgrade/blob/main/c1_sequenceDiagram.jpg?raw=true)

Packages ([Dzidek.Net.AutoUpgrade.Service](https://www.nuget.org/packages/Dzidek.Net.AutoUpgrade.Service) and [Dzidek.Net.AutoUpgrade.Upgrader](https://www.nuget.org/packages/Dzidek.Net.AutoUpgrade.Upgrader)) allow us to implement a service with an automatic update on demand if part of our application is installed outside our servers where we do not have administrator rights or even log in

## How it works
AutoUpgrade has two services (one with the ".Service" suffix and the other with the ".Upgrader" suffix). The "Service" is responsible for downloading and saving new versions in the directory. The directory where new versions are saved is watched by "Upgrader" and if a new file has been created, it stops "Service", copies new files and starts "Service".

You can control it from the outside when an update happens because it happens when you save a new version and you decide when you give a new version
## Basic usage
### Service
In appSettings.json:
```json
{
  "AutoUpgrade": {
    "ServiceName": "AutoUpgrade"
  }
}
```
In program.cs:
```csharp
builder.Host
    .UseAutoUpgradeService(builder.Configuration.GetSection("AutoUpgrade").Get<AutoUpgradeServiceConfiguration>()!);
```
Remember to set content root path for your windows service
```csharp
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
});
```
Below is an example of how to use it through the API over HTTP/HTTPS. You can use it as you want
```csharp
app.MapGet("/",
    (IAutoUpgradeService autoUpgradeService) => $"Hello World from service '{autoUpgradeService.GetVersion()}'!");
app.MapPost("/", (IFormFile file, IAutoUpgradeService autoUpgradeService) =>
{
    using var fileStream = new MemoryStream();
    file.CopyTo(fileStream);
    return autoUpgradeService.Upgrade(fileStream.ToArray(), file.FileName);
});
```
#### Configuration
- ServiceName - service name - should be the same in "Service" and "Updater", both add suffixes to distinguish the two services
- NewVersionDirectoryName - name of the directory where new versions will be saved. This directory will be watched by the Upgrader. This directory will be placed in the service directory
  - Default value: NewVersion
- ServiceNameSuffix - service suffix name
  - Default value: Service
  - Example: In the example above, the service name will be "AutoUpgrade.Service"
### Upgrader
In appSettings.json:
```json
{
  "AutoUpgrade": {
    "ServiceName": "AutoUpgrade",
    "ServicePath": "[The required path to the directory where the exe file is located]"
  }
}
```
In program.cs:
```csharp
builder.Host
    .UseAutoUpgradeUpgrader(builder.Configuration.GetSection("AutoUpgrade").Get<AutoUpgradeUpgraderConfiguration>()!);
```
Remember to set content root path for your windows service
```csharp
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
});
```
#### Configuration
- ServiceName - service name - should be the same in "Service" and "Updater", both add suffixes to distinguish the two services
- ServicePath - the required path to the directory where the exe file is located
- ServiceNameSuffix - service suffix name
  - Default value: Service
  - Example: In the example above, the service name will be "AutoUpgrade.Service"
- UpgraderNameSuffix - upgrader suffix name
  - Default value: Upgrader
  - Example: In the example above, the service name will be "AutoUpgrade.Upgrader"
- ServiceOldVersionsPath - if set, before upgrade current version will be zipped and copied to the directory

### Create service in windows
You should call with admin rights. You should first create Service because Upgrader automatically starts Service 
```
sc.exe create "AutoUpgrade.Service" binpath="[PATH]\AutoUpgrade.Service.exe" start=auto
sc.exe create "AutoUpgrade.Upgrader" binpath="[PATH]\AutoUpgrade.Upgrader.exe" start=delayed-auto
```

### Working example
The working example is available in the GitHub repository [AutoUpgrade](https://github.com/DzidekDotNet/AutoUpgrade).

For testing purposes, you need to build projects and install windows services. You can then send the new version of the service via HTTP to http://localhost:9000. You can use swagger to do this at http://localhost:9000/swagger

## Changelog
- 1.0.5
  - Prevent start service when package exists to upgrade
- 1.0.4
  - Possibility to zip and keep old versions by setting ServiceOldVersionsPath props in upgrader
- 1.0.3
  - Unzipping files with subdirectories
- 1.0.2
  - Removed unnecessary dependencies
- 1.0.1
  - DotNet6 Fix 
- 1.0.0
  - First version

## Nuget packages
### Service
[Dzidek.Net.AutoUpgrade.Service](https://www.nuget.org/packages/Dzidek.Net.AutoUpgrade.Service)
### Upgrader
[Dzidek.Net.AutoUpgrade.Upgrader](https://www.nuget.org/packages/Dzidek.Net.AutoUpgrade.Upgrader)
### Common
[Dzidek.Net.AutoUpgrade.Common](https://www.nuget.org/packages/Dzidek.Net.AutoUpgrade.Common)


## Authors

- [@DzidekDotNet](https://www.github.com/DzidekDotNet)


## License

[MIT](https://github.com/DzidekDotNet/AutoUpgrade/blob/main/LICENSE)
