# 2️⃣ Bearer Token Authentication

Bearer Token Authentication is one of the most common methods used in modern web APIs. The term "Bearer" implies that "give access to the bearer of this token."

## 🔹 How it works

The client sends a token in the HTTP Authorization header.

### Header Format
```http
Authorization: Bearer <token>
```

### Token Types
The token can be:
- **Random string:** An opaque string stored in a database (reference token).
- **JWT (JSON Web Token):** A self-contained token with data (stateless).
- **OAuth access token:** A token issued by an Authorization Server.

## 🔹 Flow

1. **Login:** The user sends credentials (username/password) to the server.
2. **Issue Token:** The server validates credentials and issues a "Bearer Token" back to the client.
3. **Store Token:** The client stores this token (e.g., in localStorage, cookie, or secure storage).
4. **Send Token:** For subsequent requests, the client sends the token in the `Authorization` header.
5. **Validate:** The server validates the token and grants access.

## 🔹 Pros
- **No password per request:** Credentials are only sent once during login.
- **Stateless possible:** If using JWTs, the server doesn't need to check the database for every request.
- **API-friendly:** Works perfectly for REST APIs and mobile backends.

## 🔹 Cons
- **Token theft = full access:** If an attacker steals the token, they can impersonate the user until the token expires.
- **Needs HTTPS:** Essential to prevents token interception.
- **Token expiration handling needed:** Clients must implement logic to handle expired tokens (often using Refresh Tokens).

## 🔹 Use cases
- **REST APIs:** The standard for securing API endpoints.
- **Mobile apps:** Native iOS and Android applications.
- **SPA (Single Page Applications):** Angular, React, Vue, etc.
