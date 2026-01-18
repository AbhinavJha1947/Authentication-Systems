using Microsoft.AspNetCore.Authentication.Negotiate;

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
