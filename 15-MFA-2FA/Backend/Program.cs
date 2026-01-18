using OtpNet;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// --- MFA / TOTP LOGIC ---

/// <summary>
/// Step 1: User completes Initial Authentication (Password)
/// We generate a secret if they don't have one, or load it from DB.
/// </summary>
app.MapPost("/api/mfa/setup", (string userId) =>
{
    // 1. Generate a random 160-bit secret (32 base32 chars)
    var secretKey = KeyGeneration.GenerateRandomKey(20);
    var base32Secret = Base32Encoding.ToString(secretKey);
    
    // 2. Generate a 'provisioning link' for QR Codes
    // format: otpauth://totp/LABEL?secret=SECRET&issuer=ISSUER
    var qrCodeUrl = $"otpauth://totp/MyApp:{userId}?secret={base32Secret}&issuer=MyApp";
    
    return Results.Ok(new { Secret = base32Secret, QrUrl = qrCodeUrl });
});

/// <summary>
/// Step 2: Verify the 6-digit code
/// </summary>
app.MapPost("/api/mfa/verify", (string code, string userId) =>
{
    // 1. Load secret from DB
    var storedSecret = "JBSWY3DPEHPK3PXP"; // Dummy
    var secretBytes = Base32Encoding.ToBytes(storedSecret);

    // 2. Calculate TOTP
    var totp = new Totp(secretBytes);
    
    // 3. Verify with a window of 1 (allows 30 seconds drift either way)
    bool isValid = totp.VerifyTotp(code, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);

    if (isValid)
    {
        return Results.Ok(new { Message = "2FA Success!" });
    }

    return Results.Unauthorized();
});

app.Run();
