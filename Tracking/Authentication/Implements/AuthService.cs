using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Authentication.Interfaces;
using Authentication.Models.Token;
using BLL.Service.Interfaces;
using Config.Configs;
using Domain.Entity.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Implements;

public class AuthService : IAuthService
{
    private readonly AuthConfig _authConfig;
    private readonly IUserService _userService;
    private readonly ISessionService _sessionService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IOptions<AuthConfig> authConfig, IUserService userService, ISessionService sessionRepo, IHttpContextAccessor httpContextAccessor)
    {
        _authConfig = authConfig.Value;
        _userService = userService;
        _sessionService = sessionRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GenerateAccessToken(User user)
    {
        var dateNow = DateTime.Now;

        var jwt = new JwtSecurityToken(
            issuer: _authConfig.Issuer,
            audience: _authConfig.Audience,
            notBefore: dateNow,
            claims: new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("password_hash", user.PasswordHash)
            },
            expires: dateNow.AddSeconds(_authConfig.LifeTimeAccessToken),
            signingCredentials: new SigningCredentials(_authConfig.SymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private string GenerateRefreshToken()
    {
        var byteArray = new byte[64];
        using var cryptoProvider = new RNGCryptoServiceProvider();
        cryptoProvider.GetBytes(byteArray);
        return Convert.ToBase64String(byteArray);
    }

    private async Task<TokenModel> SaveTokenDetails(string token, string refreshToken, User user)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var userSession = new UserSession()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTimeOffset.UtcNow,
            ExpirationDate = DateTimeOffset.UtcNow.AddSeconds(_authConfig.LifeTimeRefreshToken),
            IpAddress = ipAddress,
            IsInvalidated = false,
            Token = token,
            RefreshToken = refreshToken,
            UserId = user.Id
        };
        await _sessionService.InsertSession(userSession);

        return new TokenModel() { AccessToken = token, RefreshToken = refreshToken };
    }

    private async Task<TokenModel> GetTokens(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        return await SaveTokenDetails(accessToken, refreshToken, user);
    }
    
    public async Task<TokenModel> GetToken(TokenRequestModel authRequest)
    {
        var user = await _userService.GetUserByCredentials(authRequest.Login, authRequest.Password);

        if (user == null)
            return await Task.FromResult<TokenModel>(null);

        return await GetTokens(user);
    }

    public async Task<TokenModel> GetTokenByRefreshToken(RefreshTokenRequestModel refreshToken)
    {
        var token = GetJwtToken(refreshToken.ExpiredToken);

        var userSession = await _sessionService.GetSessionByTokens(refreshToken.ExpiredToken, refreshToken.RefreshToken);

        var validateToken = ValidateTokenInfo(token, userSession);
        if (!validateToken.IsSuccess)
            return validateToken;

        userSession.IsInvalidated = true;

        await _sessionService.Update(userSession);
        
        return await GetTokens(userSession.User);
    }

    public async Task<bool> IsTokenValid(string accessToken)
    {
        var isValid = await _sessionService.GetSessionByAccessToken(accessToken) != null;
        return await Task.FromResult(isValid);
    }

    private JwtSecurityToken GetJwtToken(string expiredToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.ReadJwtToken(expiredToken);
    }

    private TokenModel ValidateTokenInfo(JwtSecurityToken token, UserSession userSession)
    {
        if (userSession == null)
            return new TokenModel() { IsSuccess = false, Reason = "Invalid token Details" };

        if (token.ValidTo > DateTime.UtcNow)
            return new TokenModel() { IsSuccess = false, Reason = "Token not expired" };

        if (!userSession.IsActive)
            return new TokenModel() { IsSuccess = false, Reason = "Refresh token expired" };

        return new TokenModel() { IsSuccess = true };
    }
}