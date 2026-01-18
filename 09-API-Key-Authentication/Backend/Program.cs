using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
var app = builder.Build();

app.UseAuthorization();

// --- API KEY SERVICE LOGIC ---

/// <summary>
/// Professional Attribute Filter for API Key validation.
/// Usage: [ServiceFilter(typeof(ApiKeyAttribute))] or [ApiKey]
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string API_KEY_HEADER = "X-Api-Key";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 1. Check for header
        if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // 2. Resolve Configuration
        var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var validKey = config["ApiKeys:MainApp"];

        // 3. Comparison
        if (string.IsNullOrEmpty(validKey) || !validKey.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}

// Protected Endpoint
app.MapGet("/api/v1/public-data", () => Results.Ok(new { Data = "Global Statistics" }))
   .AddEndpointFilter<ApiKeyEndpointFilter>(); // Using Minimal API Filter

app.Run();

/// <summary>
/// Minimal API implementation of the API Key filter.
/// </summary>
public class ApiKeyEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var key))
            return Results.Unauthorized();

        // Validation logic here...
        
        return await next(context);
    }
}
