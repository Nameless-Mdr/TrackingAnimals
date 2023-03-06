using DAL.Base;
using Domain.Entity.Location;

namespace DAL.Interfaces;

public interface ILocationRepo : IBaseRepo<long, Location>
{
    
}