# 8️⃣ Session-Based Authentication (Traditional Web)

This is the traditional way of handling authentication, widely used before the rise of SPAs and stateless APIs.

## 🔹 Flow
1. **User logs in:** Client sends credentials.
2. **Server Validates & Creates Session:**
   - Server validates credentials.
   - Server creates a **Session** object and stores it in memory, database, or Redis.
   - Server generates a unique **Session ID**.
3. **Cookie Sent:** The Session ID is sent back to the client in a **Set-Cookie** header (usually `HTTPOnly`).
4. **Browser Stores Cookie:** The browser automatically stores this cookie and includes it in all future requests to that domain.
5. **Server Verifies:** On every request, the server reads the Session ID from the cookie, looks up the session in the DB/Store, and identifies the user.

## 🔹 Pros
- **Easy logout:** Determining "Active" users is easy. The server can simply delete the session from the store to forcibly log a user out.
- **Revocable:** Immediate ban/revocation is possible.
- **Simple for MVC apps:** Frameworks like ASP.NET Core, Django, Spring MVC handle this automatically.
- **Secure Cookies:** `HTTPOnly` and `Secure` flags prevent XSS attacks from reading the token.

## 🔹 Cons ❌
- **Not stateless:** The server must store state. If you have 1 million logged-in users, you need memory/storage for 1 million sessions.
- **Hard to scale:** If you have multiple servers (Server A, Server B), you need a shared session store (like Redis) so Server B knows about the session created on Server A (Sticky Sessions).
- **Not ideal for APIs:** Mobile apps and external clients don't handle cookies as natively or securely as browsers do.
- **CSRF Vulnerability:** Susceptible to Cross-Site Request Forgery attacks (requires Anti-CSRF tokens).

## 🔹 Use cases
- **Server-rendered apps:** Traditional websites.
- **ASP.NET MVC / Spring MVC / Rails / Django:** Standard web applications.
- **Banking / High Security:** Where immediate session revocation is critical.
