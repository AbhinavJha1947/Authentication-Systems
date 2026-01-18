var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
var app = builder.Build();

app.MapPost("/login/magic", async (string email, IMemoryCache cache) => // In real app inject IEmailService
{
    // Generate secure token
    var token = Guid.NewGuid().ToString();
    
    // Store in cache for 10 minutes
    cache.Set(token, email, TimeSpan.FromMinutes(10));
    
    var link = $"https://myapp.com/callback?token={token}";
    
    // await emailService.SendAsync(email, "Login Link", $"Click here: {link}");
    
    return Results.Ok($"Link sent! (Simulated: {link})");
});

app.MapGet("/callback", (string token, IMemoryCache cache) => 
{
    if (cache.TryGetValue(token, out string email))
    {
        cache.Remove(token); // One-time use
        
        // Issue JWT for 'email'
        // var jwt = GenerateJwtForUser(email);
        
        return Results.Ok(new { AccessToken = "simulated_jwt_token" });
    }
    return Results.BadRequest("Invalid or expired link");
});

app.Run();
