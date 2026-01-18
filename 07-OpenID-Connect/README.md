# 7️⃣ OpenID Connect (OIDC)

> [!IMPORTANT]
> **OIDC is an Authentication layer on top of OAuth 2.0.**
>
> - **OAuth 2.0** = Authorization (Can I access this?)
> - **OIDC** = Authentication (Who am I?)

## 🔹 What it is
OIDC standardizes how authentication works using OAuth. It adds a specific token called the **ID Token**.

## 🔹 Adds: The ID Token
- **Format:** JWT (JSON Web Token).
- **Content:** Contains user identity information (Claims) like `sub` (subject/user ID), `name`, `email`, `picture`.

## 🔹 Flow (Simplified)
1. **Login:** App redirects user to Identity Provider (e.g., Google).
2. **Auth:** User logs in at Google.
3. **Response:** Google redirects back to App with:
   - `Access Token` (for Authorization / API access)
   - `ID Token` (for Authentication / User Info)
4. **Verify:** App decodes `ID Token` to know *who* the user is and show their name/profile.

## 🔹 Use cases
- **Modern Authentication Systems:** The standard for most modern "Log in with X" buttons.
- **SaaS Platforms:** Auth0, Okta, AWS Cognito all use OIDC heavily.
- **Mobile & Web Apps:** Verifying identity in a standard way across different platforms.

## 🔹 Example Providers
- Login with Google
- Login with Apple
- Login with Azure AD
