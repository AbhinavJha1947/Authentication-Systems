using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.MvcCore;
using ITfoxtec.Identity.Saml2.Schemas;

var builder = WebApplication.CreateBuilder(args);

// --- SAML 2.0 CONFIGURATION ---

builder.Services.Configure<Saml2Configuration>(builder.Configuration.GetSection("Saml2"));

builder.Services.AddSaml2(options =>
{
    // SP Entity ID is the unique name for YOUR app
    options.SPOptions.EntityId = new EntityId("https://myapp.com/saml-metadata");
});

var app = builder.Build();

/// <summary>
/// Step 1: Initiate Login (Redirect to IdP)
/// </summary>
app.MapGet("/api/saml/login", (IOptions<Saml2Configuration> config) =>
{
    var binding = new Saml2RedirectBinding();
    var authnRequest = new Saml2AuthnRequest(config.Value)
    {
        AssertionConsumerServiceUrl = new Uri("https://myapp.com/api/saml/acs"),
    };

    return binding.Bind(authnRequest).ToActionResult();
});

/// <summary>
/// Step 2: Handle Response from IdP (ACS)
/// </summary>
app.MapPost("/api/saml/acs", async (HttpRequest request, IOptions<Saml2Configuration> config) =>
{
    var binding = new Saml2PostBinding();
    var samlResponse = new Saml2AuthnResponse(config.Value);

    // 1. Read and Validate Signature
    binding.ReadSamlResponse(request.ToGenericHttpRequest(), samlResponse);
    if (samlResponse.Status != Saml2StatusCodes.Success)
    {
        return Results.Unauthorized();
    }

    // 2. Successfully authenticated!
    var claimsIdentity = samlResponse.ClaimsIdentity;
    // Issue app session cookie/JWT here
    
    return Results.Ok(new { User = claimsIdentity.Name });
});

app.Run();
