using DAL.Interfaces;
using Domain.Entity.Animal;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AnimalRepo : IAnimalRepo
{
    private readonly DataContext _context;

    public AnimalRepo(DataContext context)
    {
        _context = context;
    }

    public async Task<long> Create(Animal entity)
    {
        await _context.Animals.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<Animal>> GetAllModels()
    {
        return await _context.Animals.AsNoTracking().Include(x => x.Types).Include(x => x.VisitLocations).ToListAsync();
    }

    public async Task<long> Update(Animal entity)
    {
        var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (animal == null)
            throw new Exception("Animal with such id not found");
        
        if (animal.LifeStatus == "DEAD" && entity.LifeStatus == "ALIVE")
            throw new Exception("Animal dead");
        
        var firstLoc = await _context.VisitLocations.AsNoTracking()
            .OrderBy(x => x.DateTimeOfVisitLocationPoint).ThenBy(x => x.AnimalId)
            .FirstOrDefaultAsync(x => x.AnimalId == entity.Id);

        if (firstLoc != null && firstLoc.LocationPointId == entity.ChippingLocationId)
            throw new Exception("Chipping location is first in list visited locations");

        animal.Weight = entity.Weight;
        animal.Length = entity.Length;
        animal.Height = entity.Height;
        animal.Gender = entity.Gender;
        animal.LifeStatus = entity.LifeStatus;
        if(animal.ChipperId != entity.ChipperId || animal.ChippingLocationId != entity.ChippingLocationId)
            animal.ChippingDateTime = DateTimeOffset.UtcNow;
        animal.ChipperId = entity.ChipperId;
        animal.ChippingLocationId = entity.ChippingLocationId;
        animal.DeathDateTime = entity.DeathDateTime;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> Delete(long id)
    {
        var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == id);

        if (animal == null)
            throw new Exception("Animal with such id not found");

        _context.Animals.Remove(animal);
        return await _context.SaveChangesAsync() > 0;
    }
}