using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// --- SSO CONFIGURATION (OIDC CLIENT) ---

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    // 1. The centralized Auth Server (SSO Provider)
    options.Authority = "https://sso.enterprise.com"; 
    
    // 2. Client registration details
    options.ClientId = "app_dashboard_001";
    options.ClientSecret = "app_secret_abc";
    options.ResponseType = OpenIdConnectResponseType.Code;

    // 3. Request specific scopes
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.Scope.Add("roles");

    // 4. Token handling
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;

    // 5. SSO Behavior: Don't prompt again if session exists
    options.Prompt = "none"; // Try silent auth first
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Welcome to the SSO-enabled Dashboard!").RequireAuthorization();

app.Run();
