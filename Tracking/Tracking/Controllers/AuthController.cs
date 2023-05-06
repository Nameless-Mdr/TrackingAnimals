using Authentication.Interfaces;
using Authentication.Models.Token;
using Microsoft.AspNetCore.Mvc;

namespace Tracking.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Token([FromBody] TokenRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new TokenModel() {IsSuccess = false, Reason = "Email and password must be provided."});
        var authResponse = await _authService.GetToken(model);
        if (authResponse == null)
            return Unauthorized();
        return Ok(authResponse);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new TokenModel() {IsSuccess = false, Reason = "Tokens must be provided."});
        var authResponse = await _authService.GetTokenByRefreshToken(model);
        if (authResponse == null)
            return Unauthorized();
        return Ok(authResponse);
    }
}