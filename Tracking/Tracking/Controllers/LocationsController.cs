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
public class LocationsController : Controller
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
    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        return await _locationService.GetAllModels();
    }

    [HttpPut]
    public async Task<int> UpdateLocation(Location location)
    {
        return await _locationService.Update(location);
    }

    [HttpDelete]
    public async Task<bool> DeleteLocation(int id)
    {
        return await _locationService.Delete(id);
    }
}