using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class TextPlainOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var consumesTextPlainAttribute = context.MethodInfo.GetCustomAttributes(true)
            .OfType<ConsumesAttribute>()
            .Any(a => a.ContentTypes.Contains("text/plain"));

        if (consumesTextPlainAttribute)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["text/plain"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "string"
                        }
                    }
                }
            };
        }
    }
}