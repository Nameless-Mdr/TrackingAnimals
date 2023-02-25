using DAL.Interfaces;
using Domain.Entity.Location;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class LocationRepo : ILocationRepo
{
    private readonly DataContext _context;

    public LocationRepo(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Location entity)
    {
        await _context.Locations.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<Location>> GetAllModels()
    {
        return await _context.Locations.AsNoTracking().ToListAsync();
    }

    public async Task<int> Update(Location entity)
    {
        _context.Locations.Update(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> Delete(int id)
    {
        var location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);

        if (location == null)
            throw new Exception("Location with such id not found");

        _context.Locations.Remove(location);
        return await _context.SaveChangesAsync() > 0;
    }
}