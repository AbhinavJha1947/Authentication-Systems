using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// --- SESSION CONFIGURATION ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyAuthSession";
        options.Cookie.HttpOnly = true;     // Prevents XSS
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Requires HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict; // Prevents CSRF
        
        options.LoginPath = "/api/auth/login";
        options.AccessDeniedPath = "/api/auth/forbidden";
        
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true; // Refresh session window on activity
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// --- LOGIN ---
app.MapPost("/api/auth/login", async (HttpContext context, LoginReq req) =>
{
    if (req.Username != "bob" || req.Password != "password")
        return Results.Unauthorized();

    var claims = new[] { 
        new Claim(ClaimTypes.Name, "Bob"),
        new Claim(ClaimTypes.Role, "Manager")
    };
    
    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    // This creates the session and sends the "Set-Cookie" header
    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    return Results.Ok(new { Message = "Session Created" });
});

// --- LOGOUT ---
app.MapPost("/api/auth/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Ok(new { Message = "Session Destroyed" });
});

app.Run();

public record LoginReq(string Username, string Password);
