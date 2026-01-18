# 3️⃣ JWT (JSON Web Token)

JWT (JSON Web Token) is an open standard (RFC 7519) that defines a compact and self-contained way for securely transmitting information between parties as a JSON object.

> [!NOTE]
> JWT is usually used **WITH** Bearer Auth scheme (`Authorization: Bearer <JWT>`).

## 🔹 What it is

A JWT is a string comprising three parts separated by dots (`.`):

`HEADER.PAYLOAD.SIGNATURE`

### Example
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.
SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

### Structure
1. **Header:** Algorithm & token type (e.g., HMAC SHA256, JWT).
2. **Payload:** The claims (data) about the entity (user).
   ```json
   {
     "sub": "123",
     "role": "ADMIN",
     "exp": 1700000000
   }
   ```
3. **Signature:** Verifies that the sender of the JWT is who it says it is and to ensure that the message wasn't changed along the way.

## 🔹 Flow

1. **User logs in:** Client sends credentials.
2. **Server issues JWT:** Server creates a JWT signed with a secret key (HMAC) or public/private key (RSA/ECDSA) and sends it to the client.
3. **Client sends JWT:** Client sends the JWT in the Authorization header on every request.
4. **Server verifies:** Server validates the signature using the key. **No database lookup is required** to verify the token itself (though you might check for revocation).

## 🔹 Pros
- **Stateless:** The server doesn't need to keep a session store. All necessary info is in the token.
- **Fast:** Reduced latency since no DB lookup is needed for authentication.
- **Scales very well:** Perfect for distributed systems where different services need to verify the user.
- **Cross-domain / CORS:** easier to handle than cookies in some scenarios.

## 🔹 Cons ❌
- **Cannot revoke easily:** Since the server validates the signature mathematically, it's hard to invalidate a specific token before it expires without maintaining a blacklist (which negates the stateless benefit).
- **Token size:** Can get large if you put too much info in the payload, increasing bandwidth usage.
- **If stolen → valid till expiry:** Security risk if the expiration time is too long.

## 🔹 Use cases
- **Microservices:** Passing identity between services.
- **Distributed systems:** Where centralized session storage is a bottleneck.
- **API gateways:** Validating requests before passing them to backend services.
