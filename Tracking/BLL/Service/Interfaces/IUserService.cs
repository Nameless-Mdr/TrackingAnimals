using BLL.Service.Base;
using Domain.Entity.User;

namespace BLL.Service.Interfaces;

public interface IUserService : IBaseService<int, User>
{
    public Task<IEnumerable<User>> GetUsersByParams(string firstName = "%", string lastName = "%", string email = "%", int skip = 0, int take = 10);

    public Task<User> GetUserById(int id);
}