# 💻 API Key Code Examples

## 🟢 C# / .NET 8 (Backend)

API Keys are often handled via Middleware or an Action Filter.

### 1. Middleware Approach

```csharp
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEY = "X-API-KEY";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = appSettings.GetValue<string>("ApiKey");

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        await _next(context);
    }
}

// In Program.cs
app.UseMiddleware<ApiKeyMiddleware>();
```

### 2. Attribute Filter (Cleaner for specific Controllers)

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var potentialApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration["ApiKey"];

        if (!apiKey.Equals(potentialApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}

// Usage
[ApiKey]
[HttpGet("secure")]
public IActionResult Get() => Ok("Secure");
```

## 🔴 Angular (Frontend)

Adding the key to headers.

### 1. Interceptor

```typescript
export const apiKeyInterceptor: HttpInterceptorFn = (req, next) => {
  const apiKey = 'abc123xyz789'; // In real app, load from environment
  
  const cloned = req.clone({
    setHeaders: {
      'X-API-KEY': apiKey
    }
  });

  return next(cloned);
};
```
