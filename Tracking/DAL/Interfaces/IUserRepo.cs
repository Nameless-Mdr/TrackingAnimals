using DAL.Base;
using Domain.Entity.User;

namespace DAL.Interfaces;

public interface IUserRepo : IBaseRepo<User>
{
    public Task<User> GetUserByCredentials(string login, string password);
}