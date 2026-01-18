using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

// --- KERBEROS / WINDOWS AUTH CONFIG ---
// This requires the server to be domain-joined.
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate(); 

builder.Services.AddAuthorization(options =>
{
    // Force authentication on all endpoints by default
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/internal/userinfo", (ClaimsPrincipal user) =>
{
    // In Kerberos, the Identity Name is usually "DOMAIN\UserName"
    return Results.Ok(new
    {
        AuthType = user.Identity?.AuthenticationType, // Should be "Kerberos" or "Negotiate"
        Identity = user.Identity?.Name,
        Groups = user.Claims.Where(c => c.Type == ClaimTypes.GroupSid).Select(c => c.Value)
    });
});

app.Run();
/*
   NOTE: For Kerberos to work on Windows/IIS:
   1. The 'Windows Authentication' module must be enabled in IIS.
   2. 'Anonymous Authentication' should be disabled.
   3. The Service Principal Name (SPN) must be registered for the service account.
*/
