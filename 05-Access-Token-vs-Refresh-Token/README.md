# 5️⃣ Access Token vs Refresh Token

Understanding the distinction between these two token types is crucial for secure authentication implementations, particularly with OAuth 2.0 and JWTs.

## 🔹 Access Token

- **Lifespan:** Short-lived (typically 5–15 minutes).
- **Purpose:** Used to authenticate API requests. It is sent in the `Authorization` header.
- **Security:** Because it is sent frequently, it has a higher risk of interception. Keeping the lifespan short minimizes the window of opportunity for an attacker if the token is stolen.

## 🔹 Refresh Token

- **Lifespan:** Long-lived (days, months, or even years).
- **Purpose:** Used **ONLY** to acquire a new Access Token from the Authorization Server when the current Access Token expires.
- **Security:** Stored more securely (e.g., HTTPOnly cookie) and only sent to the dedicated `/token` endpoint, not every API call.

## 🔹 Why is this needed?

1. **Security / Damage Control:** If an **Access Token** leaks, it's only valid for a few minutes. By the time an attacker tries to use it extensively, it might have already expired.
2. **User Experience:** The **Refresh Token** allows the user to stay logged in ("Remember Me") without having to re-enter credentials every 15 minutes. The app silently refreshes the session in the background.
3. **Revocation:** You can revoke a Refresh Token server-side (blocking future access), whereas stateless Access Tokens (like JWTs) cannot be easily revoked until they expire.

## 🔹 Typical Flow
1. User logs in → Server returns `Access Token` + `Refresh Token`.
2. App uses `Access Token` to fetch data.
3. 10 minutes later, `Access Token` expires (API returns 401).
4. App sends `Refresh Token` to `/refresh-token` endpoint.
5. Server verifies `Refresh Token`, issues **new** `Access Token` (and optionally a new `Refresh Token`).
6. App retries the failed request with the new `Access Token`.
