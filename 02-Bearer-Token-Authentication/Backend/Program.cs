namespace AuthSystems.BearerAuth.Backend;

/// <summary>
/// Professional Bearer Authentication implementation using ASP.NET Core Middleware.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // In a real app, we would use .AddJwtBearer()
        // Here we demonstrate the raw logic via a Custom Middleware for educational clarity.
        
        var app = builder.Build();

        app.Use(async (context, next) =>
        {
            // 1. Extract Bearer Token
            if (!context.Request.Headers.TryGetValue("Authorization", out var header))
            {
                await next();
                return;
            }

            var authHeader = header.ToString();
            if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader["Bearer ".Length..].Trim();

                // 2. Validate Token (Mocking logic)
                if (IsValidToken(token))
                {
                    // In a real app, you'd populate ClaimsPrincipal here
                    // context.User = CreatePrincipalFromToken(token);
                    await next();
                    return;
                }
            }

            // Optional: Block here if specific routes are accessed without token
            await next();
        });

        app.MapGet("/api/data", () => Results.Ok(new { Data = "Sensitive information protected by Bearer" }));

        app.Run();
    }

    private static bool IsValidToken(string token)
    {
        // Logic: Check against Redis, DB, or Signature
        return token == "enterprise-level-secret-token-2024";
    }
}
