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
        return await _context.Animals.AsNoTracking().Include(x => x.Types).ToListAsync();
    }

    public async Task<long> Update(Animal entity)
    {
        var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (animal == null)
            throw new Exception("Animal with such id not found");
        
        if (animal.LifeStatus == "DEAD" && entity.LifeStatus == "ALIVE")
            throw new Exception("Animal dead");

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