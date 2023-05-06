using DAL.Interfaces;
using Domain.Entity.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SessionRepo : ISessionRepo
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionRepo(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<Guid> InsertSession(UserSession session)
    {
        await _context.UserSessions.AddAsync(session);

        await _context.SaveChangesAsync();

        return session.Id;
    }

    public async Task<UserSession> GetSessionById(Guid id)
    {
        var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.Id == id);

        return session;
    }

    public async Task<UserSession> GetSessionByTokens(string expiredToken, string refreshToken)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var session = await _context.UserSessions
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == expiredToken
                                      && x.RefreshToken == refreshToken
                                      && x.IpAddress == ipAddress
                                      && x.IsInvalidated == false);

        return session;
    }

    public async Task<UserSession> GetSessionByAccessToken(string expiredToken)
    {
        var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var session = await _context.UserSessions
            .FirstOrDefaultAsync(x => x.Token == expiredToken && x.IpAddress == ipAddress);

        return session;
    }

    public async Task<Guid> Update(UserSession updateSession)
    {
        _context.UserSessions.Update(updateSession);

        await _context.SaveChangesAsync();

        return updateSession.Id;
    }
}