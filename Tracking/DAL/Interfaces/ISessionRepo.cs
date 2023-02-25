using Domain.Entity.User;

namespace DAL.Interfaces;

public interface ISessionRepo
{
    public Task<Guid> InsertSession(UserSession session);

    public Task<UserSession> GetSessionById(Guid id);

    public Task<UserSession> GetSessionByRefreshToken(Guid refreshToken);

    public Task<Guid> UpdateRefreshToken(UserSession updateSession);
}