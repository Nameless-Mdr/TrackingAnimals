using DAL.Base;
using Domain.Entity.Animal;

namespace DAL.Interfaces;

public interface IAnimalRepo : IBaseRepo<long, Animal>
{
    
}