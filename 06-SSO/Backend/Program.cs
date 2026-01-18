builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://my-identity-server.com"; // The SSO Provider
    options.ClientId = "my-client-app";
    options.ClientSecret = "secret";
    options.ResponseType = "code";

    options.SaveTokens = true; // Store tokens in cookie
    options.GetClaimsFromUserInfoEndpoint = true;
    
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("api1");
});
