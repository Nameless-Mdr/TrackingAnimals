using DAL.Interfaces;
using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SessionRepo : ISessionRepo
{
    private readonly DataContext _context;

    public SessionRepo(DataContext context)
    {
        _context = context;
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

        if (session == null)
            throw new Exception("Session is not found");

        return session;
    }

    public async Task<UserSession> GetSessionByTokens(string expiredToken, string refreshToken)
    {
        var session = await _context.UserSessions
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == expiredToken && x.RefreshToken == refreshToken);

        if (session == null)
            throw new Exception("Session is not found");

        return session;
    }

    public async Task<UserSession> GetSessionByIpAddress(string expiredToken, string ipAddress)
    {
        var session = await _context.UserSessions
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == expiredToken && x.IpAddress == ipAddress);

        if (session == null)
            throw new Exception("Session is not found");

        return session;
    }

    public async Task<Guid> Update(UserSession updateSession)
    {
        _context.UserSessions.Update(updateSession);

        await _context.SaveChangesAsync();

        return updateSession.Id;
    }
}