using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AuthSystems.BasicAuth.Backend;

/// <summary>
/// Entry point for Basic Authentication demonstration.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Register Custom Authentication Handler
        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        // Protected Endpoint
        app.MapGet("/api/secure", () => Results.Ok(new { Message = "Access Granted to Basic Auth Storage!" }))
           .RequireAuthorization();

        app.Run();
    }
}

/// <summary>
/// Custom Handler to process Basic Auth Header: "Authorization: Basic [Base64]"
/// </summary>
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder) { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // 1. Check for Authorization Header
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        try
        {
            var authHeader = Request.Headers.Authorization.ToString();
            if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Invalid Authorization Scheme");

            // 2. Extract and Decode Base64
            var base64Part = authHeader.Substring("Basic ".Length).Trim();
            var credentialBytes = Convert.FromBase64String(base64Part);
            var decodedCredentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            if (decodedCredentials.Length != 2)
                return AuthenticateResult.Fail("Invalid Credentials Format");

            var username = decodedCredentials[0];
            var password = decodedCredentials[1];

            // 3. Validation Logic (Mocking DB check)
            if (IsValidUser(username, password))
            {
                var claims = new[] { 
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User") 
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid Username or Password");
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Error Parsing Credentials: {ex.Message}");
        }
    }

    private bool IsValidUser(string username, string password)
    {
        // Replace with actual Database/Secret check
        return username == "admin" && password == "password123";
    }
}
