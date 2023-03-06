using DAL.Base;
using Domain.Entity.Animal;
using Type = Domain.Entity.Animal.Type;

namespace DAL.Interfaces;

public interface ITypeRepo : IBaseRepo<long, Type>
{
    public Task<Type> GetTypeById(long id);
}