using BLL.Service.Base;
using Domain.Entity.Animal;
using Type = Domain.Entity.Animal.Type;

namespace BLL.Service.Interfaces;

public interface ITypeService : IBaseService<long, Type>
{
    public Task<Type> GetTypeById(long id);
}