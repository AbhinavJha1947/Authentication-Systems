using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyAppSession";
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true; // Renew cookie if active
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Login Endpoint
app.MapPost("/login", async (HttpContext context, LoginModel model) =>
{
    if (model.Username == "admin" && model.Password == "password")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");

        await context.SignInAsync("MyCookieAuth", new ClaimsPrincipal(claimsIdentity));
        
        return Results.Ok();
    }
    return Results.Unauthorized();
});

// Logout Endpoint
app.MapPost("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync("MyCookieAuth");
    return Results.Ok();
});

app.Run();

public record LoginModel(string Username, string Password);
