using Common;
using DAL.Interfaces;
using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepo : IUserRepo
{
    private readonly DataContext _context;

    public UserRepo(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<User>> GetAllModels()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<int> Update(User entity)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (user == null)
            throw new Exception("User with such id not found");

        user.FirstName = entity.FirstName;
        user.LastName = entity.LastName;
        user.Email = entity.Email;
        user.PasswordHash = entity.PasswordHash;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> Delete(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new Exception("User with such id not found");
        
        _context.Users.Remove(user);
        return  await _context.SaveChangesAsync() > 0;
    }

    public async Task<User> GetUserByCredentials(string login, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == login.ToLower());

        if (user == null)
            throw new Exception("user not found");

        if (!HashHelper.Verify(password, user.PasswordHash))
            throw new Exception("password is incorrect");

        return user;
    }
}