using DAL.Base;
using Domain.Entity.User;

namespace DAL.Interfaces;

public interface IUserRepo : IBaseRepo<int, User>
{
    public Task<User> GetUserByCredentials(string login, string password);
}