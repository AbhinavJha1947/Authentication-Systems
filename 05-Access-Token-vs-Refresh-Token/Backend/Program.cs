using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// --- THE LOGIC ---

/// <summary>
/// Endpoint to exchange a Refresh Token for a new Access Token.
/// </summary>
app.MapPost("/api/auth/refresh", async ([FromBody] RefreshRequest request) => 
{
    // 1. Find token in DB
    // var storedToken = await db.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);
    
    // Mock Validation
    bool isValid = (request.RefreshToken == "valid_rotation_token_001");
    bool isExpired = false;

    if (!isValid || isExpired)
        return Results.Unauthorized();

    // 2. Perform ROTATION (Best Practice)
    // var newRefreshToken = GenerateSecureToken();
    // storedToken.Token = newRefreshToken;
    // await db.SaveChangesAsync();

    // 3. Generate New Short-lived Access Token
    // var newAccessToken = JwtUtil.CreateToken(user, expiry: Minutes(15));
    
    return Results.Ok(new {
        AccessToken = "new_short_lived_jwt_token",
        RefreshToken = "next_rotation_token_002"
    });
});

app.Run();

public record RefreshRequest(string AccessToken, string RefreshToken);
