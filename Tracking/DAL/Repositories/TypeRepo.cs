using DAL.Interfaces;
using Domain.Entity.Animal;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class TypeRepo : ITypeRepo
{
    private readonly DataContext _context;

    public TypeRepo(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(TypeAnimal entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<TypeAnimal>> GetAllModels()
    {
        return await _context.Types.AsNoTracking().ToListAsync();
    }

    public async Task<int> Update(TypeAnimal entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> Delete(int id)
    {
        var type = await _context.Types.FirstOrDefaultAsync(x => x.Id == id);

        if (type == null)
            throw new Exception("Type with such id not found");

        _context.Types.Remove(type);
        return await _context.SaveChangesAsync() > 0;
    }
}