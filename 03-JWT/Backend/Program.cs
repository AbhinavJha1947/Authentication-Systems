using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt; // Added missing using
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Authentication Services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://myapp.com",
        ValidAudience = "https://myapp.com",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("SuperSecretKey12345678901234567890")) // > 256 bits
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// 2. Enable Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/me", (ClaimsPrincipal user) =>
{
    return $"Hello, {user.Identity?.Name}";
})
.RequireAuthorization();

app.MapPost("/login", (LoginModel model) =>
{
    if (model.Username == "user" && model.Password == "pass")
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "user1"),
            new Claim(JwtRegisteredClaimNames.Email, "user@example.com"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey12345678901234567890"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://myapp.com",
            audience: "https://myapp.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return Results.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
    return Results.Unauthorized();
});

app.Run();

public record LoginModel(string Username, string Password);
