using DAL.Interfaces;
using Domain.Entity.Location;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class VisitLocationRepo : IVisitLocationRepo
{
    private readonly DataContext _context;

    public VisitLocationRepo(DataContext context)
    {
        _context = context;
    }

    public async Task<long> Create(VisitLocation entity)
    {
        var lastLoc = await _context.VisitLocations.AsNoTracking()
            .OrderByDescending(x => x.DateTimeOfVisitLocationPoint).ThenBy(x => x.AnimalId)
            .FirstOrDefaultAsync(x => x.AnimalId == entity.AnimalId);

        if (lastLoc != null)
        {
            if (lastLoc.LocationPointId == entity.LocationPointId)
                throw new Exception("Trying to add previous point id");
        }
        else
        {
            var animal = await _context.Animals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.AnimalId);

            if (animal == null)
                throw new Exception("Animal with such id not found");

            if (animal.ChippingLocationId == entity.LocationPointId)
                throw new Exception("Trying to add chipping location to visit location");
        }

        await _context.VisitLocations.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<VisitLocation>> GetAllModels()
    {
        return await _context.VisitLocations.AsNoTracking().OrderBy(x => x.DateTimeOfVisitLocationPoint)
            .ToListAsync();
    }

    public async Task<long> Update(VisitLocation entity)
    {
        var curLoc = await _context.VisitLocations.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (curLoc == null)
            throw new Exception("Location with such id not found");

        if (curLoc.LocationPointId == entity.LocationPointId)
            throw new Exception("This location point id already set");
        
        var animal = await _context.Animals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.AnimalId);
        if (animal == null)
            throw new Exception("Animal with such id not found");

        var locations = await _context.VisitLocations.AsNoTracking().OrderBy(x => x.DateTimeOfVisitLocationPoint)
            .ThenBy(x => x.AnimalId).ToListAsync();

        var firstLoc = locations.FirstOrDefault(x => x.AnimalId == entity.AnimalId);
        if (firstLoc != null && firstLoc.Id == entity.Id && animal.ChippingLocationId == entity.LocationPointId)
            throw new Exception("Trying to update first visit location on chipping location");

        var nextLoc = locations
            .SkipWhile(x => x.AnimalId != entity.AnimalId || x.DateTimeOfVisitLocationPoint <= curLoc.DateTimeOfVisitLocationPoint)
            .FirstOrDefault();
        if (nextLoc != null && nextLoc.LocationPointId == entity.LocationPointId)
            throw new Exception("Trying to update next visit location on same point id");
        
        var prevLoc = locations
            .OrderByDescending(x => x.DateTimeOfVisitLocationPoint).ThenBy(x => x.AnimalId)
            .SkipWhile(x => x.AnimalId != entity.AnimalId || x.DateTimeOfVisitLocationPoint >= curLoc.DateTimeOfVisitLocationPoint)
            .FirstOrDefault();
        if (prevLoc != null && prevLoc.LocationPointId == entity.LocationPointId)
            throw new Exception("Trying to update prev visit location on same point id");

        curLoc.AnimalId = entity.AnimalId;
        curLoc.LocationPointId = entity.LocationPointId;

        await _context.SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task<bool> Delete(long id)
    {
        var location = await _context.VisitLocations.FirstOrDefaultAsync(x => x.Id == id);

        if (location == null)
            throw new Exception("Location with such id not found");

        _context.VisitLocations.Remove(location);
        
        var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == location.AnimalId);
        var firstLoc = await _context.VisitLocations.AsNoTracking().OrderBy(x => x.DateTimeOfVisitLocationPoint)
            .ThenBy(x => x.AnimalId).Skip(1).FirstOrDefaultAsync(x => x.AnimalId == animal!.Id);

        if (firstLoc != null && firstLoc.LocationPointId == animal!.ChippingLocationId)
            _context.VisitLocations.Remove(firstLoc);
        
        return await _context.SaveChangesAsync() > 0;
    }
}