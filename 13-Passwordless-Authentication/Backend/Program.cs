using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
var app = builder.Build();

// --- PASSWORDLESS (MAGIC LINK) LOGIC ---

/// <summary>
/// Step 1: User requests a login link.
/// </summary>
app.MapPost("/api/auth/magic-link", (string email, IMemoryCache cache) =>
{
    // 1. Generate a unique, unpredictable token
    var token = Guid.NewGuid().ToString("N");
    
    // 2. Store token in cache (linked to email) for 15 minutes
    cache.Set($"magic_{token}", email, TimeSpan.FromMinutes(15));
    
    // 3. Construct the link (In a real app, send via IEmailService)
    var magicLink = $"https://myapp.com/login-callback?token={token}";
    
    // Log for demo purposes
    Console.WriteLine($"[EMAIL SENT TO {email}]: {magicLink}");

    return Results.Ok(new { Message = "Magic link sent to your inbox!" });
});

/// <summary>
/// Step 2: User clicks the link, and browser hits this endpoint.
/// </summary>
app.MapGet("/api/auth/verify-token", (string token, IMemoryCache cache) =>
{
    var key = $"magic_{token}";
    
    if (cache.TryGetValue(key, out string? email))
    {
        // 1. Invalidate token (One-time use)
        cache.Remove(key);

        // 2. Token is valid! Issue a real session (JWT)
        var jwt = "simulated_secure_jwt_token_for_" + email;
        
        return Results.Ok(new { Token = jwt });
    }

    return Results.Unauthorized();
});

app.Run();
