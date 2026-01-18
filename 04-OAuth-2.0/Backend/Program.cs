using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "Google";
})
.AddCookie("Cookies")
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = new PathString("/signin-google");
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Trigger Challenge
app.MapGet("/login-google", async (HttpContext context) =>
{
    await context.ChallengeAsync("Google", new AuthenticationProperties 
    { 
        RedirectUri = "/" 
    });
});

app.Run();
