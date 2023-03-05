using BLL.Service.Interfaces;
using DAL.Interfaces;
using Domain.Entity.Animal;

namespace BLL.Service.Implements;

public class TypeService : ITypeService
{
    private readonly ITypeRepo _typeRepo;

    public TypeService(ITypeRepo typeRepo)
    {
        _typeRepo = typeRepo;
    }

    public async Task<int> Create(TypeAnimal entity)
    {
        return await _typeRepo.Create(entity);
    }

    public async Task<IEnumerable<TypeAnimal>> GetAllModels()
    {
        return await _typeRepo.GetAllModels();
    }

    public async Task<int> Update(TypeAnimal entity)
    {
        return await _typeRepo.Update(entity);
    }

    public async Task<bool> Delete(int id)
    {
        return await _typeRepo.Delete(id);
    }
}