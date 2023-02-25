using Domain.Entity.User;

namespace BLL.Service.Interfaces;

public interface ISessionService
{
    public Task<UserSession> GetSessionById(Guid id);
}