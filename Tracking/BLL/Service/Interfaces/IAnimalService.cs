using BLL.Service.Base;
using Domain.Entity.Animal;

namespace BLL.Service.Interfaces;

public interface IAnimalService : IBaseService<long, Animal>
{
    public Task<IEnumerable<Animal>> GetAnimalByParams(DateTimeOffset? startDate, DateTimeOffset? endDate, int chipperId = 0,
        int chippingLocationId = 0, string lifeStatus = "", string gender = "", int skip = 0, int take = 10);

    public Task<Animal> GetAnimalById(long id);
}