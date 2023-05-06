using BLL.Service.Interfaces;
using DAL.Interfaces;
using Domain.Entity.User;

namespace BLL.Service.Implements;

public class SessionService : ISessionService
{
    private readonly ISessionRepo _sessionRepo;

    public SessionService(ISessionRepo sessionRepo)
    {
        _sessionRepo = sessionRepo;
    }

    public async Task<Guid> InsertSession(UserSession session)
    {
        return await _sessionRepo.InsertSession(session);
    }

    public async Task<UserSession> GetSessionById(Guid id)
    {
        return await _sessionRepo.GetSessionById(id);
    }

    public async Task<UserSession> GetSessionByTokens(string expiredToken, string refreshToken)
    {
        return await _sessionRepo.GetSessionByTokens(expiredToken, refreshToken);
    }

    public async Task<UserSession> GetSessionByAccessToken(string expiredToken)
    {
        return await _sessionRepo.GetSessionByAccessToken(expiredToken);
    }

    public async Task<Guid> Update(UserSession updateSession)
    {
        return await _sessionRepo.Update(updateSession);
    }
}