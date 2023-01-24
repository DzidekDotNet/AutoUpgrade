
# Windows service with auto upgrade feature

An auto-updating Windows service with the ability to send a new compressed version on demand
![C1 and sequence diagram](https://github.com/DzidekDotNet/AutoUpgrade.Service/blob/main/c1_sequenceDiagram.jpg?raw=true)

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
Below is an example of how to use it through the API over HTTP/HTTPS. You can use it as you want
```csharp
app.MapGet("/",
    (IAutoUpgradeService autoUpgradeService) => $"Hello World from service '{autoUpgradeService.GetVersion()}'!");
app.MapPost("/", (IFormFile file, IAutoUpgradeService autoUpgradeService) => autoUpgradeService.Upgrade(file));
```
#### Configuration
- Service name - service name - should be the same in "Service" and "Updater", both add suffixes to distinguish the two services
- New Version Directory Name - name of the directory where new versions will be saved. This directory will be watched by the Upgrader. This directory will be placed in the service directory
  - Default value: NewVersion
- ServiceNameSuffix - service suffix name
  - Default value: Service
  - Example: In the example above, the service name will be "AutoUpgrade.Service"
### Upgrader

### Create service in windows
You should call with admin rights. You should first create Service because Upgrader automatically starts Service 
```
sc.exe create "AutoUpgrade.Service" binpath="[PATH]\AutoUpgrade.Service.exe" start=auto
sc.exe create "AutoUpgrade.Upgrader" binpath="[PATH]\AutoUpgrade.Upgrader.exe" start=delayed-auto
```

## Changelog
- 7.0.0 and 6.0.0
    - First version

## Versioning policy
The project major version will be the same as the DotNetCore version

## Nuget
### Service
[Dzidek.Net.AutoUpgrade.Service](https://www.nuget.org/packages/Dzidek.Net.AutoUpgrade.Service)
### Upgrader
[Dzidek.Net.AutoUpgrade.Upgrader](https://www.nuget.org/packages/Dzidek.Net.AutoUpgrade.Upgrader)


## Authors

- [@DzidekDotNet](https://www.github.com/DzidekDotNet)


## License

[MIT](https://github.com/DzidekDotNet/AutoUpgrade.Service/blob/main/LICENSE)
