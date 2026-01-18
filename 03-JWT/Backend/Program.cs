using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- JWT CONFIGURATION ---
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Encoding.UTF8.GetBytes("Your-Very-Long-And-Secure-Secret-Key-32-Chars!");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://auth.company.com",
            ValidAudience = "https://api.company.com",
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ClockSkew = TimeSpan.Zero // Remove default 5 mins buffer
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// --- LOGIN ENDPOINT (Token Generation) ---
app.MapPost("/api/auth/login", (LoginRequest request) =>
{
    // 1. Mock Validation
    if (request.User != "admin" || request.Pass != "pass") 
        return Results.Unauthorized();

    // 2. Prepare Claims
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, "user_001"),
        new Claim(JwtRegisteredClaimNames.Email, "admin@company.com"),
        new Claim(ClaimTypes.Role, "SuperAdmin"),
        new Claim("tier", "premium") // Custom claim
    };

    // 3. Create Token
    var key = new SymmetricSecurityKey(secretKey);
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "https://auth.company.com",
        audience: "https://api.company.com",
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(30),
        signingCredentials: creds
    );

    return Results.Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
});

app.Run();

public record LoginRequest(string User, string Pass);
