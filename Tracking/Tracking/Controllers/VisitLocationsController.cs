using AutoMapper;
using BLL.Service.Interfaces;
using Domain.DTO.VisitLocation;
using Domain.Entity.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tracking.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class VisitLocationsController : Controller
{
    private readonly IVisitLocationService _visitLocationService;
    private readonly IMapper _mapper;

    public VisitLocationsController(IVisitLocationService visitLocationService, IMapper mapper)
    {
        _visitLocationService = visitLocationService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<long> CreateVisitLocation([FromForm] CreateVisitLocationModel model)
    {
        return await _visitLocationService.Create(_mapper.Map<VisitLocation>(model));
    }

    [HttpGet]
    public async Task<IEnumerable<GetVisitLocationModel>> GetAllVisitLocations()
    {
        var locations = await _visitLocationService.GetAllModels();
        return locations.Select(x => _mapper.Map<GetVisitLocationModel>(x));
    }

    [HttpPut]
    public async Task<long> UpdateVisitLocation([FromForm] UpdateVisitLocationModel model)
    {
        return await _visitLocationService.Update(_mapper.Map<VisitLocation>(model));
    }

    [HttpDelete]
    public async Task<bool> DeleteVisitLocation([FromForm] long id)
    {
        return await _visitLocationService.Delete(id);
    }
}