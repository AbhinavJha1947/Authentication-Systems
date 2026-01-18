public class TokenRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expiry { get; set; }
    public bool IsRevoked { get; set; } 
    public string UserId { get; set; }
}

// In Program.cs or Controller
app.MapPost("/api/refresh", async (TokenRequest request, MyDbContext db) =>
{
    var existingRefreshToken = await db.RefreshTokens
        .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

    if (existingRefreshToken == null || 
        existingRefreshToken.Expiry < DateTime.UtcNow || 
        existingRefreshToken.IsRevoked)
    {
        return Results.Unauthorized();
    }

    // Generate NEW Access Token
    var newAccessToken = GenerateAccessToken(existingRefreshToken.UserId);
    
    // Rotate Refresh Token (Optional but Recommended)
    var newRefreshToken = GenerateRefreshToken();
    
    existingRefreshToken.IsRevoked = true; // Revoke old one
    db.RefreshTokens.Add(newRefreshToken);
    await db.SaveChangesAsync();

    return Results.Ok(new 
    { 
        accessToken = newAccessToken, 
        refreshToken = newRefreshToken.Token 
    });
});
