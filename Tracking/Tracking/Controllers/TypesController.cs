using AutoMapper;
using BLL.Service.Interfaces;
using Domain.DTO.TypeAnimal;
using Domain.Entity.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tracking.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class TypesController : ControllerBase
{
    private readonly ITypeService _typeService;

    private readonly IMapper _mapper;

    public TypesController(ITypeService typeService, IMapper mapper)
    {
        _typeService = typeService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<int> CreateType([FromForm] CreateTypeModel model)
    {
        return await _typeService.Create(_mapper.Map<TypeAnimal>(model));
    }

    [HttpGet]
    public async Task<IEnumerable<GetTypeModel>> GetAllTypes()
    {
        var types = await _typeService.GetAllModels();
        return types.Select(type => _mapper.Map<GetTypeModel>(type));
    }

    [HttpPut]
    public async Task<int> UpdateType([FromForm] GetTypeModel entity)
    {
        var type = new TypeAnimal()
        {
            Id = entity.Id,
            Type = entity.Type
        };
        
        return await _typeService.Update(type);
    }

    [HttpDelete]
    public async Task<bool> DeleteType([FromForm] int id)
    {
        return await _typeService.Delete(id);
    }
}