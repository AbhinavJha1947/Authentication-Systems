using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace AuthSystems.OAuth2.Backend;

/// <summary>
/// Professional OAuth 2.0 integration (Google) for .NET 8.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Configure Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie() // Local cookie session after OAuth success
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "YOUR_ID";
            googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "YOUR_SECRET";
            
            // Scopes define what data we want to access
            googleOptions.Scope.Add("profile");
            googleOptions.Scope.Add("email");
            
            // Save tokens to the authentication cookie for later use
            googleOptions.SaveTokens = true;
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        // 2. Auth Endpoints
        app.MapGet("/api/auth/external-login", async (HttpContext context) =>
        {
            // Triggers redirect to Google
            await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = "/api/auth/callback"
            });
        });

        app.MapGet("/api/auth/callback", async (HttpContext context) =>
        {
            var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var claims = result.Principal.Identities.FirstOrDefault()?.Claims
                    .Select(claim => new { claim.Type, claim.Value });
                return Results.Ok(new { Message = "LoggedIn", User = claims });
            }
            return Results.BadRequest("OAuth failed");
        });

        app.Run();
    }
}
