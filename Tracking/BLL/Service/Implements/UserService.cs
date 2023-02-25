using BLL.Service.Interfaces;
using DAL.Interfaces;
using Domain.Entity.User;

namespace BLL.Service.Implements;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<int> Create(User entity)
    {
        return await _userRepo.Create(entity);
    }

    public async Task<IEnumerable<User>> GetAllModels()
    {
        return await _userRepo.GetAllModels();
    }

    public async Task<int> Update(User entity)
    {
        return await _userRepo.Update(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _userRepo.Delete(id);
    }
}