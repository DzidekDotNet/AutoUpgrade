using AutoUpgradeService.Service;
using Dzidek.Net.AutoUpgrade.Service;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Service", Version = "v1" });
    options.OperationFilter<SwaggerFileOperationFilter>();
});
builder.WebHost
    .UseKestrel();
builder.Host
    .UseAutoUpgradeService(builder.Configuration.GetSection("AutoUpgrade").Get<AutoUpgradeServiceConfiguration>()!);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service v1"));

app.MapGet("/",
    (IAutoUpgradeService autoUpgradeService) => $"Hello World from service '{autoUpgradeService.GetVersion()}'!");
app.MapPost("/", (IFormFile file, IAutoUpgradeService autoUpgradeService) =>
{
    using var fileStream = new MemoryStream();
    file.CopyTo(fileStream);
    return autoUpgradeService.Upgrade(fileStream.ToArray(), file.FileName);
});

app.Run();