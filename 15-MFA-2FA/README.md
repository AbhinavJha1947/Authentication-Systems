# 1️⃣5️⃣ MFA / 2FA (Multi-Factor Authentication)

**Authentication Factors:** To prove who you are, valid authentication requires more than one evidence from different categories.

## 🔹 The 3 Main Factors
1. **Something you KNOW:** Password, PIN, Security Question.
2. **Something you HAVE:** Phone (SMS/App), Hardware Key (YubiKey), Smart Card.
3. **Something you ARE:** Fingerprint, FaceID, Iris scan.

> **2FA (Two-Factor Auth):** Using exactly two different factors (e.g., Password + SMS Code).
> **MFA (Multi-Factor Auth):** Using two or more factors.

> [!NOTE]
> Using two passwords is NOT 2FA. That is just two of "Something you know".

## 🔹 Common Methods

### 1. SMS / Email OTP
- **Pros:** Ubiquitous, easy to understand.
- **Cons:** Vulnerable to SIM Swapping and phishing.

### 2. Authenticator Apps (TOTP)
- **Examples:** Google Authenticator, Microsoft Authenticator, Authy.
- **Tech:** Time-based One-Time Password (TOTP). Generating a 6-digit code offline based on a shared secret and the current time.
- **Pros:** More secure than SMS, works offline.
- **Cons:** User needs a smartphone.

### 3. Push Notifications
- **Flow:** User enters password → Phone pops up "Are you trying to sign in?" → User taps "Approve".
- **Pros:** Best UX.
- **Cons:** "MFA Fatigue" (User blindly approving request just to stop the buzzing).

### 4. Hardware Security Keys (U2F / FIDO2)
- **Examples:** YubiKey, Titan Key.
- **Pros:** Phishing-resistant. Highest security standard.
- **Cons:** Cost ($) and physical logistics (plugging it in).

## 🔹 Use cases
- **Everywhere:** It is now a standard recommendation for ALL accounts (Social Media, Banking, Email, Enterprise).
