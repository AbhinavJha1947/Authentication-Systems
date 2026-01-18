# 💻 Kerberos (Windows Auth) Code Examples

## 🟢 C# / .NET 8 (Backend)

Uses `Microsoft.AspNetCore.Authentication.Negotiate`.

### 1. Setup

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate(); // Handles Kerberos/NTLM

builder.Services.AddAuthorization(options =>
{
    // By default, all requests require value user
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (HttpContext context) => 
    $"Hello, {context.User.Identity.Name}!"); // Output: DOMAIN\User

app.Run();
```

### 2. IIS Configuration (`web.config`)
If running behind IIS, you must enable Windows Authenticaion in IIS and disable Anonymous Authentication.

## 🔴 Angular (Frontend)

Windows Authentication is handled by the **Browser** automatically in Intranet settings.
1. Browser receives `401 Unauthorized` with header `WWW-Authenticate: Negotiate`.
2. Browser automatically sends the current Windows User credentials (ticket) in the header.
3. No Javascript code is needed.
