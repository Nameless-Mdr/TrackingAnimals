using BLL.Service.Interfaces;
using DAL.Interfaces;
using Domain.Entity.Location;

namespace BLL.Service.Implements;

public class VisitLocationService : IVisitLocationService
{
    private readonly IVisitLocationRepo _visitLocationRepo;

    public VisitLocationService(IVisitLocationRepo visitLocationRepo)
    {
        _visitLocationRepo = visitLocationRepo;
    }

    public async Task<long> Create(VisitLocation entity)
    {
        return await _visitLocationRepo.Create(entity);
    }

    public async Task<IEnumerable<VisitLocation>> GetAllModels()
    {
        return await _visitLocationRepo.GetAllModels();
    }

    public async Task<long> Update(VisitLocation entity)
    {
        return await _visitLocationRepo.Update(entity);
    }

    public async Task<bool> Delete(long id)
    {
        return await _visitLocationRepo.Delete(id);
    }
}