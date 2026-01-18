using Microsoft.AspNetCore.Authentication.Certificate;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// --- MTLS CONFIGURATION ---
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options =>
    {
        // 1. Specify which certificates we trust
        options.AllowedCertificateTypes = CertificateTypes.All;
        
        // 2. Custom validation events
        options.Events = new CertificateAuthenticationEvents
        {
            OnCertificateValidated = context =>
            {
                // Verify against a known list or thumbprint
                var expectedThumbprint = "CF43...YOUR_THUMBPRINT";
                if (context.ClientCertificate.Thumbprint != expectedThumbprint)
                {
                    context.Fail("Certificate not recognized in our allow-list.");
                    return Task.CompletedTask;
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, context.ClientCertificate.Subject),
                    new Claim(ClaimTypes.Name, "Internal-Machine-01"),
                    new Claim("CertificateSerial", context.ClientCertificate.SerialNumber)
                };

                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                context.Success();
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/m2m/sync", () => Results.Ok(new { Status = "Securely Synced via mTLS" }))
   .RequireAuthorization();

app.Run();
