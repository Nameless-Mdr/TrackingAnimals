using AutoMapper;
using BLL.Service.Interfaces;
using Domain.DTO.Animal;
using Domain.Entity.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Type = Domain.Entity.Animal.Type;

namespace Tracking.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService _animalService;
    private readonly ITypeService _typeService;

    private readonly IMapper _mapper;

    public AnimalsController(IAnimalService animalService, IMapper mapper, ITypeService typeService)
    {
        _animalService = animalService;
        _mapper = mapper;
        _typeService = typeService;
    }

    [HttpPost]
    public async Task<long> CreateAnimal([FromForm] CreateAnimalModel model)
    {
        var animal = _mapper.Map<Animal>(model);
        animal.Types = new List<Type>();
        foreach (var id in model.TypesId)
        {
            var type = await _typeService.GetTypeById(id);
            animal.Types.Add(type);
        }
        return await _animalService.Create(animal);
    }

    [HttpGet]
    public async Task<IEnumerable<GetAnimalModel>> GetAllAnimals()
    {
        var animals = await _animalService.GetAllModels();
        var models = animals.Select(x => _mapper.Map<GetAnimalModel>(x));
        return models;
    }

    [HttpPut]
    public async Task<long> UpdateAnimal([FromForm] UpdateAnimalModel model)
    {
        var animal = _mapper.Map<Animal>(model);
        return await _animalService.Update(animal);
    }

    [HttpDelete]
    public async Task<bool> DeleteAnimal([FromForm] long id)
    {
        return await _animalService.Delete(id);
    }
}