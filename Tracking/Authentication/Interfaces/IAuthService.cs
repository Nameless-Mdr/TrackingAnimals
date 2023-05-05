using Authentication.Models.Token;

namespace Authentication.Interfaces;

public interface IAuthService
{
    public Task<TokenModel> GetToken(TokenRequestModel authRequest);

    public Task<TokenModel> GetTokenByRefreshToken(RefreshTokenRequestModel refreshToken);

    public Task<bool> IsTokenValid(string accessToken, string ipAddress);
}