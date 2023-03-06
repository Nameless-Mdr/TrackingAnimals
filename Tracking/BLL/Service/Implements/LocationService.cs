using BLL.Service.Interfaces;
using DAL.Interfaces;
using Domain.Entity.Location;

namespace BLL.Service.Implements;

public class LocationService : ILocationService
{
    private readonly ILocationRepo _locationRepo;

    public LocationService(ILocationRepo locationRepo)
    {
        _locationRepo = locationRepo;
    }

    public Task<long> Create(Location entity)
    {
        return _locationRepo.Create(entity);
    }

    public Task<IEnumerable<Location>> GetAllModels()
    {
        return _locationRepo.GetAllModels();
    }

    public Task<long> Update(Location entity)
    {
        return _locationRepo.Update(entity);
    }

    public Task<bool> Delete(long id)
    {
        return _locationRepo.Delete(id);
    }
}