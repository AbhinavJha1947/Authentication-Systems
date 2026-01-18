# 💻 Biometric Auth (WebAuthn) Code Examples

## 🔴 Angular (Frontend)

Biometrics on the web use the **Web Authentication API (WebAuthn)**.

### 1. Registration (Enrollment)

```typescript
async register() {
  const publicKeyCredentialCreationOptions: PublicKeyCredentialCreationOptions = {
    challenge: Uint8Array.from("random-challenge-from-server", c => c.charCodeAt(0)),
    rp: {
      name: "My App",
      id: "localhost",
    },
    user: {
      id: Uint8Array.from("user-id", c => c.charCodeAt(0)),
      name: "user@example.com",
      displayName: "User Name",
    },
    pubKeyCredParams: [{ alg: -7, type: "public-key" }],
    authenticatorSelection: {
      authenticatorAttachment: "platform", // Use built-in (FaceID/TouchID)
    },
    timeout: 60000,
    attestation: "direct"
  };

  const credential = await navigator.credentials.create({
    publicKey: publicKeyCredentialCreationOptions
  });

  // Send 'credential' to backend to verify and store public key
  console.log(credential);
}
```

### 2. Login (Assertion)

```typescript
async login() {
  const publicKeyCredentialRequestOptions: PublicKeyCredentialRequestOptions = {
    challenge: Uint8Array.from("new-random-challenge", c => c.charCodeAt(0)),
    timeout: 60000,
    rpId: "localhost",
  };

  const assertion = await navigator.credentials.get({
    publicKey: publicKeyCredentialRequestOptions
  });

  // Send 'assertion' to backend to verify signature
  console.log(assertion);
}
```

## 🟢 C# / .NET 8 (Backend)

You need a library like `Fido2.Net` to verify the cryptographic signatures.

```csharp
// Pseudo-code using Fido2.Net library
public async Task<IActionResult> MakeAssertion([FromBody] AuthenticatorAssertionRawResponse clientResponse)
{
    // Get registered public key from DB
    var storedKey = _db.Keys.Find(userId);
    
    // Verify
    var res = await _fido2.MakeAssertionAsync(clientResponse, options, storedKey);
    
    if (res.Status == "ok") {
        // Issue JWT / Cookie
    }
}
```
