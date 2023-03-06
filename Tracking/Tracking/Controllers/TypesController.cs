using AutoMapper;
using BLL.Service.Interfaces;
using Domain.DTO.TypeAnimal;
using Domain.Entity.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Type = Domain.Entity.Animal.Type;

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
    public async Task<long> CreateType([FromForm] CreateTypeModel model)
    {
        return await _typeService.Create(_mapper.Map<Type>(model));
    }

    [HttpGet]
    public async Task<IEnumerable<GetTypeModel>> GetAllTypes()
    {
        var types = await _typeService.GetAllModels();
        return types.Select(type => _mapper.Map<GetTypeModel>(type));
    }

    [HttpPut]
    public async Task<long> UpdateType([FromForm] GetTypeModel entity)
    {
        var type = new Type()
        {
            Id = entity.Id,
            NameType = entity.NameType
        };
        
        return await _typeService.Update(type);
    }

    [HttpDelete]
    public async Task<bool> DeleteType([FromForm] long id)
    {
        return await _typeService.Delete(id);
    }
}