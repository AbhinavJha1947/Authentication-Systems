# 8️⃣ Session-Based Authentication

The "Traditional" method. The server statefully remembers the user session, and the browser uses a cookie (Session ID) to identify itself.

## 🔹 Sequence Diagram

```mermaid
sequenceDiagram
    participant Browser
    participant Server
    participant DB as Session Store (Redis/DB)
    
    Browser->>Server: POST /login (Credentials)
    Server->>Server: Validate Credentials
    Server->>DB: Store Session (ID: 123, User: 'Bob')
    Server-->>Browser: Set-Cookie: SID=123; HttpOnly; Secure
    
    Note over Browser: Browser stores cookie automatically
    
    Browser->>Server: GET /profile + Cookie: SID=123
    Server->>DB: Lookup Session 123
    DB-->>Server: Return User 'Bob'
    Server-->>Browser: 200 OK (Bob's Profile)
```

## 🔹 Key Features
- **Stateful**: The server *must* track sessions.
- **Cookies**: Relies on browser-native cookie handling.
- **Immediate Revocation**: Deleting a session from the DB logs the user out immediately across all devices.

## 🔹 Common Pitfalls ❌
- **CSRF**: Since browsers send cookies automatically, websites are vulnerable to Cross-Site Request Forgery.
- **Scalability**: Requires a shared session store (like Redis) for multiple server instances.
- **Mobile Apps**: Native apps don't handle cookies as easily as browsers, making this less ideal for mobile backends.

## 🔹 Industry Best Practices ✅
1.  **HttpOnly**: Always set the `HttpOnly` flag to prevent XSS from stealing the session ID.
2.  **SameSite=Strict**: Use the `SameSite` attribute to mitigate CSRF attacks.
3.  **Redis Store**: Use an in-memory store like Redis for session data to ensure high performance and scalability.

## 🔹 Interview Tips 💡
- **Q: How does Token-based auth differ from Session-based auth?**
  - A: Session-based is **stateful** (server stores data). Token-based is **stateless** (client stores data).
- **Q: What is a "Sticky Session"?**
  - A: It's a load balancer setting that sends a user to the same server that created their session. It's an alternative to a shared session store but makes scaling harder.
- **Q: What happens if the Session Store goes down?**
  - A: All users are logged out because the server can no longer verify their session IDs.
