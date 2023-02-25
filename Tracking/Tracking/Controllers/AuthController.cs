using BLL.Models.Token;
using BLL.Service.Interfaces;
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
    public async Task<TokenModel> Token(TokenRequestModel model)
    {
        return await _authService.GetToken(model.Login, model.Password);
    }

    [HttpPost]
    public async Task<TokenModel> RefreshToken(RefreshTokenRequestModel model)
    {
        return await _authService.GetTokenByRefreshToken(model.RefreshToken);
    }
}