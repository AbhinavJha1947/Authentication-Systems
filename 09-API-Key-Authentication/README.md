# 9️⃣ API Key Authentication

## 🔹 How it works
The client includes a static key in the request, usually in the header or query string.

### Header Example
```http
X-API-KEY: abc123xyz789
```

### Query Parameter Example
```http
https://api.example.com/data?api_key=abc123xyz789
```

## 🔹 Pros
- **Very simple:** Easiest way to limit access to an API.
- **Good for machine-to-machine:** Simple scripts or daemon processes.
- **Tracking:** Easy to track usage quotas per key (Rate Limiting).

## 🔹 Cons
- **No user identity:** The key authenticates the *application* or *client*, not a specific *user*.
- **No fine-grained access:** Usually gives "all or nothing" access unless complex scopes are built on top.
- **Weak security:** Keys are often long-lived and hard to rotate. If they are embedded in client-side code (mobile app, frontend JS), they **will** be stolen.

## 🔹 Use cases
- **Public APIs:** Google Maps API, Weather APIs.
- **Rate limiting:** Identifying a caller to enforce tiered usage plans.
- **Internal services:** Simple service-to-service communication within a secured network.

> [!WARNING]
> **Never** leave API keys in client-side code (React/Angular apps) or public repositories.
