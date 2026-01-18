# 4️⃣ OAuth 2.0 (Authorization Framework)

> [!IMPORTANT]
> **OAuth is NOT authentication by itself.** It is a framework for **delegated authorization**.
> It answers the question: "Can this application access these resources on behalf of this user?"

## 🔹 Example
"Allow **Google** to access my **GitHub repos**?"
"Log in with **Facebook**" (using OAuth for identity, often coupled with OIDC).

## 🔹 Main Roles
1. **Resource Owner:** The User (You).
2. **Client:** The Application trying to access the account (e.g., A smart TV app).
3. **Authorization Server:** The Server validating credentials and issuing tokens (e.g., Google Auth Server).
4. **Resource Server:** The API hosting the user's data (e.g., Google Drive API, Gmail API).

## 🔹 Grant Types (Flows)

| Grant Type | Use Case | Description |
| :--- | :--- | :--- |
| **Authorization Code** | Web apps (Server-side) | The most common and secure flow. Client gets a code, exchanges it for a token. |
| **PKCE** (Proof Key for Code Exchange) | Mobile / SPA | Extension of Auth Code for public clients (Single Page Apps, Mobile) preventing code interception. |
| **Client Credentials** | Server-to-server | Machine-to-machine communication where no user is involved. |
| **Password** (Deprecated) | Legacy | Sending username/password directly. **Avoid**. |

## 🔹 Tokens
- **Access Token:** Short-lived token used to access the API.
- **Refresh Token:** Long-lived token used to get a new Access Token when the old one expires.

## 🔹 Pros
- **Secure:** The client application never sees the user's password.
- **Industry Standard:** Supported by all major identity providers (Google, Facebook, Microsoft, etc.).
- **Granular Access:** Scopes allow limiting what the app can do (e.g., "read-only" access).

## 🔹 Cons
- **Complex:** The flows can be difficult to implementation correctly from scratch.
- **Configuration Heavy:** Requires registering apps, managing redirects, client secrets, etc.

## 🔹 Use cases
- **Social Login:** "Sign in with Google/Apple".
- **Third-party integrations:** Allowing an app to write to your calendar.
- **Enterprise Auth:** Managing internal access across many apps.
