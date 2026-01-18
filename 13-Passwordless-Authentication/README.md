# 1️⃣3️⃣ Passwordless Authentication

A method of verifying a user's identity without requiring them to remember or enter a password.

## 🔹 Types

### 1. Magic Link
- User enters email.
- System sends a unique, one-time-use link (e.g., `app.com/login?token=xyz`).
- User clicks link → Logged in.

### 2. OTP (One-Time Password)
- User enters phone number/email.
- System sends a 4-6 digit code.
- User enters code → Logged in.

### 3. FIDO2 / WebAuthn (Passkeys)
- Using the device's built-in authenticator (Fingerprint, FaceID, YubiKey) to sign a challenge from the server.
- This is the future of passwordless.

## 🔹 Pros
- **Better UX:** Frictionless login.
- **No Password Management:** Users don't need to remember complex passwords or use managers.
- **Less Phishing:** Harder to phish a Magic Link (if it expires fast) or WebAuthn (impossible to phish).

## 🔹 Cons
- **Dependencies:** Relies on Email/SMS delivery (which can be delayed or blocked).
- **SIM Swapping:** SMS OTP is vulnerable to SIM swap attacks.
- **Device Loss:** If you lose your phone/email access, recovery can be harder.

## 🔹 Use cases
- **Consumer Apps:** Slack, Medium (Magic Links).
- **Banking:** OTP for transactions.
- **Modern Web Apps:** Moving towards Passkeys.
