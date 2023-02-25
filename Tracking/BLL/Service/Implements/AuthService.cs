using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL.Models.Token;
using BLL.Service.Interfaces;
using Config.Configs;
using DAL.Interfaces;
using Domain.Entity.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Service.Implements;

public class AuthService : IAuthService
{
    private readonly AuthConfig _authConfig;

    private readonly IUserRepo _userRepo;

    private readonly ISessionRepo _sessionRepo;

    public AuthService(IOptions<AuthConfig> authConfig, IUserRepo userRepo, ISessionRepo sessionRepo)
    {
        _authConfig = authConfig.Value;
        _userRepo = userRepo;
        _sessionRepo = sessionRepo;
    }

    public async Task<TokenModel> GetToken(string login, string password)
    {
        var user = await _userRepo.GetUserByCredentials(login, password);

        var session = new UserSession
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            RefreshToken = Guid.NewGuid(),
            Created = DateTimeOffset.UtcNow,
            User = user
        };

        await _sessionRepo.InsertSession(session);

        return GenerateTokens(session);
    }

    public async Task<TokenModel> GetTokenByRefreshToken(string refreshToken)
    {
        var validParams = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = _authConfig.SymmetricSecurityKey()
        };

        var principal = new JwtSecurityTokenHandler().ValidateToken(refreshToken, validParams, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtToken
            || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("invalid token");
        }

        if (principal.Claims.FirstOrDefault(x => x.Type == "refreshToken")?.Value is String refreshString &&
            Guid.TryParse(refreshString, out var refreshId))
        {
            var session = await _sessionRepo.GetSessionByRefreshToken(refreshId);

            if (!session.IsActive)
            {
                throw new Exception("session not active");
            }

            session.RefreshToken = Guid.NewGuid();
            await _sessionRepo.UpdateRefreshToken(session);

            return GenerateTokens(session);
        }

        throw new SecurityTokenException("invalid token");
    }
    
    private TokenModel GenerateTokens(UserSession session)
    {
        var dateNow = DateTime.Now;

        if (session == null)
            throw new Exception("session not found");

        var jwt = new JwtSecurityToken(
            issuer: _authConfig.Issuer,
            audience: _authConfig.Audience,
            notBefore: dateNow,
            claims: new Claim[]
            {
                new("displayName", session.User.FirstName),
                new("sessionId", session.Id.ToString()),
                new("id", session.User.Id.ToString()!)
            },
            expires: dateNow.AddMinutes(_authConfig.LifeTime),
            signingCredentials: new SigningCredentials(_authConfig.SymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var jwrRefresh = new JwtSecurityToken(
            notBefore: dateNow,
            claims: new Claim[]
            {
                new("refreshToken", session.RefreshToken.ToString())
            },
            expires: dateNow.AddMinutes(_authConfig.LifeTime),
            signingCredentials: new SigningCredentials(_authConfig.SymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        var encodeJwtRefresh = new JwtSecurityTokenHandler().WriteToken(jwrRefresh);

        return new TokenModel(encodeJwt, encodeJwtRefresh);
    }
}