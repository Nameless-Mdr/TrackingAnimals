using BLL.Service.Interfaces;
using DAL.Interfaces;
using Domain.Entity.Animal;

namespace BLL.Service.Implements;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepo _animalRepo;

    public AnimalService(IAnimalRepo animalRepo)
    {
        _animalRepo = animalRepo;
    }

    public async Task<long> Create(Animal entity)
    {
        return await _animalRepo.Create(entity);
    }

    public async Task<IEnumerable<Animal>> GetAllModels()
    {
        return await _animalRepo.GetAllModels();
    }

    public async Task<long> Update(Animal entity)
    {
        return await _animalRepo.Update(entity);
    }

    public async Task<bool> Delete(long id)
    {
        return await _animalRepo.Delete(id);
    }

    public async Task<IEnumerable<Animal>> GetAnimalByParams(DateTimeOffset? startDate, DateTimeOffset? endDate, int chipperId = 0,
        int chippingLocationId = 0, string lifeStatus = "", string gender = "", int skip = 0, int take = 10)
    {
        return await _animalRepo.GetAnimalByParams(startDate, endDate, chipperId, chippingLocationId, lifeStatus,
            gender, skip, take);
    }

    public async Task<Animal> GetAnimalById(long id)
    {
        return await _animalRepo.GetAnimalById(id);
    }
}