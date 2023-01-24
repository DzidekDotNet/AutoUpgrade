using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoUpgradeService.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public class SwaggerFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileUploadMime = "multipart/form-data";
        if (!HasOperationMediaType(operation, fileUploadMime))
            return;

        var fileParams = context.MethodInfo.GetParameters().Where(p => p.ParameterType == typeof(IFormFile));
        operation.RequestBody.Content[fileUploadMime].Schema.Properties =
            fileParams.ToDictionary(k => k.Name ?? "", _ => new OpenApiSchema()
            {
                Type = "string",
                Format = "binary"
            });
    }

    private static bool HasOperationMediaType(OpenApiOperation operation, string mediaType)
    {
        if (operation.RequestBody == null)
        {
            return false;
        }
        return operation.RequestBody.Content.Any(x => x.Key.Equals(mediaType, StringComparison.InvariantCultureIgnoreCase));
    }
}