using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Entity.Animal.Type;

namespace DAL.Repositories;

public class TypeRepo : ITypeRepo
{
    private readonly DataContext _context;

    public TypeRepo(DataContext context)
    {
        _context = context;
    }

    public async Task<long> Create(Type entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<Type>> GetAllModels()
    {
        return await _context.Types.AsNoTracking().ToListAsync();
    }

    public async Task<long> Update(Type entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> Delete(long id)
    {
        var type = await GetTypeById(id);

        _context.Types.Remove(type);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Type> GetTypeById(long id)
    {
        var type = await _context.Types.FirstOrDefaultAsync(x => x.Id == id);

        if (type == null)
            throw new Exception("Type with such id not found");

        return type;
    }
}