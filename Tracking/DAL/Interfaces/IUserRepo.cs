using DAL.Base;
using Domain.Entity.User;

namespace DAL.Interfaces;

public interface IUserRepo : IBaseRepo<int, User>
{
    public Task<User> GetUserByCredentials(string login, string password);

    public Task<User> GetUserById(int id);

    public Task<IEnumerable<User>> GetUsersByParams(string firstName = "", string lastName = "", string email = "", int skip = 0, int take = 10);
}