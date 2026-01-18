# 1️⃣2️⃣ Biometric Authentication

## 🔹 Examples
- **Fingerprint:** TouchID, Android Fingerprint.
- **Face ID:** Apple FaceID, Windows Hello.
- **Voice Recognition**
- **Iris Scan**

## 🔹 How it works
1. **Enrollment:** User scans their biometric data. The device creates a mathematical hash/template and stores it in a **Secure Enclave** (dedicated hardware chip). **The actual image is never stored.**
2. **Verification:** User scans again. The device compares the new hash with the stored hash.
3. **Token Release:** If it matches, the Secure Enclave releases a cryptographic key (or token) to the app.

> [!NOTE]
> **Biometrics authenticate the User to the Device, not the API.**
>
> The backend doesn't receive a fingerprint image. It normally receives a token signed by the device's private key after biometric success.

## 🔹 Pros
- **User Experience:** Extremely fast and convenient.
- **Security:** Hard to spoof (liveness checks prevent using photos/casts).
- **No forgotten passwords.**

## 🔹 Cons
- **False Positives/Negatives:** Can fail with wet fingers or masks.
- **Privacy Concerns:** Users are hesitant to give biometric data (though it stays on device usually).
- **Unchangeable:** You can reset a password, but you can't change your fingerprint.

## 🔹 Use cases
- **Mobile Banking Apps:** Login.
- **Phone Unlocking.**
- **MFA:** As a second factor ("Something you are").
