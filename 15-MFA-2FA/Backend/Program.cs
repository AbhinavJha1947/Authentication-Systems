using OtpNet; // Package: Otp.NET

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/verify-2fa", (string userCode, string userId) => // Inject DB context
{
    // var userSecret = db.Users.Find(userId).TwoFactorSecret; 
    var userSecret = "JBSWY3DPEHPK3PXP"; // Example secret
    var bytes = Base32Encoding.ToBytes(userSecret);
    
    var totp = new Totp(bytes);
    
    // Window of 30 seconds (standard)
    bool valid = totp.VerifyTotp(userCode, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);

    if (valid) return Results.Ok("Verified!");
    return Results.Unauthorized();
});

app.Run();
