using Dzidek.Net.AutoUpgrade.Upgrader;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseAutoUpgradeUpgrader(builder.Configuration.GetSection("AutoUpgrade").Get<AutoUpgradeUpgraderConfiguration>()!);
var app = builder.Build();
app.Run();