using Fido2NetLib;
using Fido2NetLib.Objects;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFido2(options =>
{
    options.ServerDomain = "localhost";
    options.ServerName = "FIDO2 App";
    options.Origin = "https://localhost:4200";
});

var app = builder.Build();

// --- WEBAUTHN / BIOMETRIC LOGIC ---

/// <summary>
/// Step 1: Server provides a CHALLENGE to the client.
/// </summary>
app.MapPost("/api/bio/make-assertion-options", (IFido2 fido2) =>
{
    var userDetails = new Fido2User { Name = "bob", Id = Encoding.UTF8.GetBytes("user_001"), DisplayName = "Bob" };
    var existingKeys = new List<PublicKeyCredentialDescriptor>(); // Load from DB

    var options = fido2.GetAssertionOptions(existingKeys, UserVerificationRequirement.Required);
    
    // Store options in session for validation step
    // HttpContext.Session.SetString("fido2_options", options.ToJson());
    
    return Results.Ok(options);
});

/// <summary>
/// Step 2: Server VERIFIES the signature from the browser.
/// </summary>
app.MapPost("/api/bio/make-assertion", async (AuthenticatorAssertionRawResponse clientResponse, IFido2 fido2) =>
{
    // 1. Get stored public key
    var storedKey = new byte[] { /* Load from DB */ };
    
    // 2. Perform Math Verification
    var res = await fido2.MakeAssertionAsync(clientResponse, originalOptions, storedKey, ...);
    
    if (res.Status == "ok")
    {
        // Issue JWT
        return Results.Ok("Logged in with Biometrics!");
    }
    
    return Results.Unauthorized();
});

app.Run();
