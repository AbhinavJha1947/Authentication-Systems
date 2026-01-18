# 1️⃣ Basic Authentication (Basic Auth)

Basic Authentication is the simplest method for enforcing access controls to web resources because it doesn't require cookies, session identifiers, or login pages.

## 🔹 How it works

The client sends the username and password encoded in Base64 in the HTTP Authorization header.

### Header Format
```http
Authorization: Basic base64(username:password)
```

> [!WARNING]
> **Base64 is ENCODING, NOT ENCRYPTION.** Anyone who intercepts the request can decode the credentials.

## 🔹 Flow

1. **Client sends Request:** The client sends an HTTP request with the `Authorization` header containing the Base64 encoded `username:password`.
2. **Server Validates:** The server decodes the Base64 string and validates the username and password against its user database.
3. **Access Granted/Denied:** If valid, the server processes the request. If invalid, it returns a `401 Unauthorized` status.

## 🔹 Pros
- **Very simple:** Extremely easy to understand and implement.
- **Easy to implement:** Supported by almost every web server and HTTP client out of the box.
- **Supported everywhere:** No special libraries needed on the client side.

## 🔹 Cons ❌
- **Credentials sent on every request:** The username and password are sent with every single API call, increasing the attack surface.
- **No logout mechanism:** Since it's stateless and relies on the browser sending the header, there's no server-side way to "log out" a user other than closing the browser or clearing the cache.
- **Unsafe without HTTPS:** Because Base64 is easily decoded, this MUST strictly be used over HTTPS (TLS).
- **Not scalable:** Can be harder to manage for large-scale user bases compared to token-based systems.

## 🔹 Use cases
- **Internal tools:** Simple scripts or internal dashboards where security risks are lower.
- **Dev/testing:** Quick setup for development environments.
- **Simple admin panels:** For low-risk administrative interfaces.

> [!CAUTION]
> **Never use Basic Auth for public production APIs.** The risk of credential compromise is too high.
