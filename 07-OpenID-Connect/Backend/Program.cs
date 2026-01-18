using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// --- OIDC CONFIGURATION ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://identity-provider.com";
    options.ClientId = "my-client-app";
    options.ClientSecret = "secret";
    options.ResponseType = "code";

    // Standard OIDC scopes
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");

    options.SaveTokens = true;
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Helper to inspect the ID Token
app.MapGet("/api/user/claims", (ClaimsPrincipal user) =>
{
    // Extract standard OIDC claims
    return Results.Ok(new
    {
        SubjectId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        Email = user.FindFirst(ClaimTypes.Email)?.Value,
        GivenName = user.FindFirst(ClaimTypes.GivenName)?.Value,
        IdToken = user.FindFirst("id_token")?.Value // If SaveTokens is true
    });
})
.RequireAuthorization();

app.Run();
