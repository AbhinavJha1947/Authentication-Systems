var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

    if (authHeader != null && authHeader.StartsWith("Bearer "))
    {
        var token = authHeader.Substring("Bearer ".Length).Trim();

        // SIMULATION: Validate token against DB/Redis
        if (token == "valid-opaque-token-123")
        {
            // Attach user to context
            // context.User = ...
            await next();
            return;
        }
    }

    // If endpoint requires auth, return 401 (this logic depends on your specific needs)
    // For this example, we just pass through, but a real auth middleware would block 
    // access to protected endpoints.
    await next();
});

app.MapGet("/protected", () => "Secret Data");

app.Run();
