# 💻 Mutual TLS (mTLS) Code Examples

## 🟢 C# / .NET 8 (Backend)

mTLS is primarily configured at the Server (Kestrel/IIS) level, but you can enforce it in the app.

### 1. Kestrel Configuration (`Program.cs`)

```csharp
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
    });
});

builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options =>
    {
        options.AllowedCertificateTypes = CertificateTypes.All; 
        
        // Custom validation logic
        options.Events = new CertificateAuthenticationEvents
        {
            OnCertificateValidated = context =>
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, 
                        context.ClientCertificate.Subject, 
                        ClaimValueTypes.String, context.Options.ClaimsIssuer),
                    new Claim(ClaimTypes.Name,
                        context.ClientCertificate.Subject,
                        ClaimValueTypes.String, context.Options.ClaimsIssuer)
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
app.Run();
```

## 🔴 Client Implementation (Code, not Browser)

M2M communication usually uses `HttpClient` with a certificate.

```csharp
var handler = new HttpClientHandler();
handler.ClientCertificates.Add(new X509Certificate2("client-cert.pfx", "password"));
var client = new HttpClient(handler);
var response = await client.GetAsync("https://mtls-server.com");
```

> **Note:** For Angular/Browsers, the **Browser** handles mTLS. When you visit the site, the browser pops up a "Select Certificate" dialog. Javascript cannot directly access the certificate.
