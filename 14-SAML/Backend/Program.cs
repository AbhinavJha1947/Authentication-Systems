// Requires library: ITfoxtec.Identity.Saml2
// Program.cs
/*
builder.Services.Configure<Saml2Configuration>(builder.Configuration.GetSection("Saml2"));

builder.Services.AddSaml2(options =>
{
    options.SPOptions.EntityId = new EntityId("https://myapp.com/saml");
});

// Login Endpoint (SP Initiated)
app.MapGet("/login", (Saml2Configuration config) =>
{
    var binding = new Saml2RedirectBinding();
    binding.SetRelayStateQuery(new Dictionary<string, string> { { "relayState", "some_state" } });
    
    // Create SAML Request
    var saml2AuthnRequest = new Saml2AuthnRequest(config);
    
    // Redirect User to IdP
    return binding.Bind(saml2AuthnRequest).ToActionResult();
});

// Assertion Consumer Service (ACS) - Where IdP posts back
app.MapPost("/acs", async (HttpRequest request, Saml2Configuration config) =>
{
    var binding = new Saml2PostBinding();
    var saml2AuthnResponse = new Saml2AuthnResponse(config);

    // Read and Validate XML Signature
    binding.ReadSamlResponse(request.ToGenericHttpRequest(), saml2AuthnResponse);
    if (saml2AuthnResponse.Status != Saml2StatusCodes.Success) 
    {
        return Results.Unauthorized();
    }

    // Authenticate User
    var claimsPrincipal = saml2AuthnResponse.ClaimsIdentity;
    // Issue app cookie or JWT
    return Results.Ok("Logged in!");
});
*/
