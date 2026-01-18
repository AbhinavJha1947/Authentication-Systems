# 💻 Passwordless Auth Code Examples

## 🟢 C# / .NET 8 (Backend)

Implementation of a "Magic Link" login.

### 1. Generate & Send Link

```csharp
app.MapPost("/login/magic", async (string email, IMemoryCache cache, IEmailService emailService) =>
{
    // Generate secure token
    var token = Guid.NewGuid().ToString();
    
    // Store in cache for 10 minutes
    cache.Set(token, email, TimeSpan.FromMinutes(10));
    
    var link = $"https://myapp.com/callback?token={token}";
    
    await emailService.SendAsync(email, "Login Link", $"Click here: {link}");
    
    return Results.Ok("Link sent!");
});
```

### 2. Verify Token

```csharp
app.MapGet("/callback", (string token, IMemoryCache cache) => 
{
    if (cache.TryGetValue(token, out string email))
    {
        cache.Remove(token); // One-time use
        
        // Issue JWT for 'email'
        var jwt = GenerateJwtForUser(email);
        
        return Results.Ok(new { AccessToken = jwt });
    }
    return Results.BadRequest("Invalid or expired link");
});
```

## 🔴 Angular (Frontend)

Handling the callback url.

### 1. Callback Component

```typescript
@Component({ ... })
export class CallbackComponent implements OnInit {
  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) {}

  ngOnInit() {
    // Extract token from URL
    const token = this.route.snapshot.queryParamMap.get('token');
    
    if (token) {
      this.http.get<{accessToken: string}>(`/api/callback?token=${token}`)
        .subscribe({
          next: (res) => {
            localStorage.setItem('access_token', res.accessToken);
            this.router.navigate(['/dashboard']);
          },
          error: () => this.router.navigate(['/login'])
        });
    }
  }
}
```
