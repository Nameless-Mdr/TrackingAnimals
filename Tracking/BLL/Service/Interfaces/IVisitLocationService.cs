using BLL.Service.Base;
using Domain.Entity.Location;

namespace BLL.Service.Interfaces;

public interface IVisitLocationService : IBaseService<long, VisitLocation>
{
    public Task<IEnumerable<VisitLocation>> GetVisitLocationByParams(DateTimeOffset? startDate, DateTimeOffset? endDate,
        int skip = 0, int take = 10);
}