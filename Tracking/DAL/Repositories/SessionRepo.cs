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
            throw new Exception("session is not found");

        return session;
    }

    public async Task<UserSession> GetSessionByRefreshToken(Guid refreshToken)
    {
        var session = await _context.UserSessions.Include(x 
            => x.User).FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        if (session == null)
            throw new Exception("session is not found");

        return session;
    }

    public async Task<Guid> UpdateRefreshToken(UserSession updateSession)
    {
        _context.UserSessions.Update(updateSession);

        await _context.SaveChangesAsync();

        return updateSession.Id;
    }
}