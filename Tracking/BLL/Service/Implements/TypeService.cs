using BLL.Service.Interfaces;
using DAL.Interfaces;
using Type = Domain.Entity.Animal.Type;

namespace BLL.Service.Implements;

public class TypeService : ITypeService
{
    private readonly ITypeRepo _typeRepo;

    public TypeService(ITypeRepo typeRepo)
    {
        _typeRepo = typeRepo;
    }

    public async Task<long> Create(Type entity)
    {
        return await _typeRepo.Create(entity);
    }

    public async Task<IEnumerable<Type>> GetAllModels()
    {
        return await _typeRepo.GetAllModels();
    }

    public async Task<long> Update(Type entity)
    {
        return await _typeRepo.Update(entity);
    }

    public async Task<bool> Delete(long id)
    {
        return await _typeRepo.Delete(id);
    }

    public async Task<Type> GetTypeById(long id)
    {
        return await _typeRepo.GetTypeById(id);
    }
}