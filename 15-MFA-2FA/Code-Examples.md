# 💻 MFA / 2FA Code Examples

## 🟢 C# / .NET 8 (Backend)

Generating and verifying TOTP (Time-based One-Time Password) for Authenticator Apps (Google Auth). Use library: `OtpHierarchy` or `GoogleAuthenticator`.

### 1. Setup Secret (Registration)

```csharp
using OtpNet; // Package: Otp.NET

// Generate Secret Key for User
var key = KeyGeneration.GenerateRandomKey(20);
var base32String = Base32Encoding.ToString(key);

// Store 'base32String' in User's DB record (Encrypted!)

// Generate QR Code URI for Google Authenticator
var uri = new OtpUri(OtpType.Totp, base32String, "user@example.com", "MyApp");
// Return 'uri' to frontend to render QR Code
```

### 2. Verify Code (Login)

```csharp
app.MapPost("/verify-2fa", (string userCode, string userId, MyDbContext db) =>
{
    var userSecret = db.Users.Find(userId).TwoFactorSecret; // "JBSWY3DPEHPK3PXP"
    var bytes = Base32Encoding.ToBytes(userSecret);
    
    var totp = new Totp(bytes);
    
    // Window of 30 seconds (standard)
    bool valid = totp.VerifyTotp(userCode, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);

    if (valid) return Results.Ok("Verified!");
    return Results.Unauthorized();
});
```

## 🔴 Angular (Frontend)

### 1. Code Input UI

Simple text input for the 6-digit code.

```typescript
verify2fa(code: string) {
  this.http.post('/api/verify-2fa', { code })
    .subscribe({
      next: () => this.router.navigate(['/home']),
      error: () => this.errorMessage = "Invalid Code"
    });
}
```
