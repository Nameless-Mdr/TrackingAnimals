using Domain.Entity.User;

namespace DAL.Interfaces;

public interface ISessionRepo
{
    public Task<Guid> InsertSession(UserSession session);

    public Task<UserSession> GetSessionById(Guid id);

    public Task<UserSession> GetSessionByTokens(string expiredToken, string refreshToken);

    public Task<UserSession> GetSessionByIpAddress(string expiredToken, string ipAddress);
        
    public Task<Guid> Update(UserSession updateSession);
}