using AutoMapper;
using BLL.Service.Interfaces;
using Domain.DTO.Location;
using Domain.Entity.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tracking.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    private readonly IMapper _mapper;

    public LocationsController(ILocationService locationService, IMapper mapper)
    {
        _locationService = locationService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<int> CreateLocation(CreateLocationModel model)
    {
        return await _locationService.Create(_mapper.Map<Location>(model));
    }

    [HttpGet]
    public async Task<IEnumerable<GetLocationModel>> GetAllLocations()
    {
        var locations = await _locationService.GetAllModels();
        return locations.Select(x => _mapper.Map<GetLocationModel>(x));
    }

    [HttpPut]
    public async Task<int> UpdateLocation([FromForm] GetLocationModel model)
    {
        var location = new Location()
        {
            Id = model.Id,
            Latitude = model.Latitude,
            Longitude = model.Longitude
        };
        return await _locationService.Update(location);
    }

    [HttpDelete]
    public async Task<bool> DeleteLocation(int id)
    {
        return await _locationService.Delete(id);
    }
}