app.MapGet("/user-info", (HttpContext context) =>
{
    if (!context.User.Identity.IsAuthenticated)
        return Results.Unauthorized();

    // The OIDC middleware automatically decodes the ID Token 
    // and populates the ClaimsPrincipal (context.User)
    
    var claims = context.User.Claims.Select(c => new { c.Type, c.Value });
    
    // You can specifically look for OIDC standard claims
    var sub = context.User.FindFirst("sub")?.Value;
    var name = context.User.FindFirst("name")?.Value;
    
    return Results.Ok(new 
    { 
        Subject = sub, 
        Name = name, 
        AllClaims = claims 
    });
})
.RequireAuthorization();
