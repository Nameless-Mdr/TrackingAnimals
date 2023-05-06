using Domain.Entity.User;

namespace BLL.Service.Interfaces;

public interface ISessionService
{
    public Task<Guid> InsertSession(UserSession session);

    public Task<UserSession> GetSessionById(Guid id);

    public Task<UserSession> GetSessionByTokens(string expiredToken, string refreshToken);
    
    public Task<UserSession> GetSessionByAccessToken(string expiredToken);

    public Task<Guid> Update(UserSession updateSession);
}