using DAL.Base;
using Domain.Entity.Location;

namespace DAL.Interfaces;

public interface IVisitLocationRepo : IBaseRepo<long, VisitLocation>
{
    public Task<IEnumerable<VisitLocation>> GetVisitLocationByParams(DateTimeOffset? startDate, DateTimeOffset? endDate,
        int skip = 0, int take = 10);
}