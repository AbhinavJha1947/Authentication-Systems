// Pseudo-code using Fido2.Net library
// Need nuget package: Fido2.Net

public class BioController : Controller {
    
    [HttpPost]
    public async Task<IActionResult> MakeAssertion([FromBody] AuthenticatorAssertionRawResponse clientResponse)
    {
        // Get registered public key from DB
        var storedKey = _db.Keys.Find(userId);
        
        // Verify
        var res = await _fido2.MakeAssertionAsync(clientResponse, options, storedKey);
        
        if (res.Status == "ok") {
            // Issue JWT / Cookie
            return Ok();
        }
        return Unauthorized();
    }
}
